using System;
using System.Net.Sockets;
namespace Xfs
{
    public class XfsPeer : XfsTcpSession
    {
        public XfsPeer()
        {
            AddComponent(new XfsPeerSession());
            AddComponent(new XfsCoolDown(this.EcsId));
            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsPeer:" + this.NodeType + ":" + this.IsServer);
        }
        public XfsPeer(NodeType nodeType)
        {
            this.IsServer = true;
            this.NodeType = nodeType;
            AddComponent(new XfsPeerSession());
            AddComponent(new XfsCoolDown(this.EcsId));

            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsPeer:" + this.NodeType + ":" + this.IsServer);
        }
        public override void OnConnect()
        {
            XfsTcpServer server = null;
            XfsSockets.XfsTcpServers.TryGetValue(this.NodeType, out server);
            if (server != null)
            {
                ///显示与客户端连接
                Console.WriteLine("{0} 客户端{1}连接成功", XfsTimerTool.CurrentTime(), Socket.RemoteEndPoint);

                ///tpeer已经加入字典
                server.TPeers.Add(this.EcsId, this);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ComponentId: " + this.EcsId + " 已经加入字典");
                ///显示客户端群中的客户端数量
                Console.WriteLine(XfsTimerTool.CurrentTime() + " TPeers Count: " + server.TPeers.Count);              
            }
        }
        public override void OnTransferParameter(object obj, XfsParameter parameter)
        {
            ///将字符串string,用json反序列化转换成MvcParameter参数
            if (parameter.TenCode == TenCode.Zero)
            {
                this.GetComponent<XfsCoolDown>().CdCount = 0;
                return;
            }
            ///将MvcParameter参数分别列队并处理
            //parameter.Keys.Clear();
            //if (parameter.Back)
            //{
            //    parameter.PeerIds.Add(this.NodeType, this.EcsId);

            //    Console.WriteLine(XfsTimerTool.CurrentTime() + " parameter.PeerIds.count: " + parameter.PeerIds.Count);
            //}

            parameter.Keys.Add(this.EcsId);

            XfsTcpServer server = null;
            XfsSockets.XfsTcpServers.TryGetValue(this.NodeType, out server);
            if (server != null)
            {
                server.Recv(parameter);
            }

        }
        public override void XfsDispose()
        {
            XfsTcpServer server = null;
            XfsSockets.XfsTcpServers.TryGetValue(this.NodeType, out server);
            if (server != null)
            {
                base.XfsDispose();
                ///删除掉心跳包群中对应的peer
                server.TPeers.Remove(EcsId);
                Console.WriteLine(XfsTimerTool.CurrentTime() + "{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 一个客户端:已经中断连接" + " TPeers: " + server.TPeers.Count);

            }
        }

    }
}