using System;
using System.Net.Sockets;
namespace Xfs
{
    public class XfsPeer : XfsTcpSession
    {
        public XfsPeer(NodeType nodeType)
        {
            this.IsServer = true;
            this.NodeType = nodeType;

            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsPeer:" + this.NodeType + ":" + this.IsServer);
        }
        public override void OnConnect()
        {
            XfsTcpServer server = XfsSockets.GetTcpServer(this.NodeType);
            if (server != null)
            {
                ///显示与客户端连接
                Console.WriteLine("{0} 客户端{1}连接成功", XfsTimerTool.CurrentTime(), Socket.RemoteEndPoint);

                ///tpeers已经加入字典
                server.TPeers.Add(this.EcsId, this);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ComponentId: " + this.EcsId + " 已经加入字典");
                ///显示客户端群中的客户端数量
                Console.WriteLine(XfsTimerTool.CurrentTime() + " TPeers Count: " + server.TPeers.Count);

                //XfsTcpSession tpeer = null;
                //bool yes1 = XfsTcpServer.Instance.TPeers.TryGetValue(this.EcsId, out tpeer);
                //if (yes1 != true)
                //{
                //    ///tpeers已经加入字典
                //    XfsTcpServer.Instance.TPeers.Add(this.EcsId, this);
                //    Console.WriteLine(XfsTimerTool.CurrentTime() + " ComponentId: " + this.EcsId + " 已经加入字典");
                //}
                /////显示客户端群中的客户端数量
                //Console.WriteLine(XfsTimerTool.CurrentTime() + " TPeers Count: " + XfsTcpServer.Instance.TPeers.Count);
            }
        }
        public override void XfsDispose()
        {
            XfsTcpServer server = XfsSockets.GetTcpServer(this.NodeType);
            if (server != null)
            {
                base.XfsDispose();
                ///删除掉心跳包群中对应的peer
                server.TPeers.Remove(EcsId);
                Console.WriteLine(XfsTimerTool.CurrentTime() + "{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 一个客户端:已经中断连接" + " TPeers: " + server.TPeers.Count);

                //    if (XfsTcpServer.Instance.NodeType == this.NodeType)
                //{
                //    base.XfsDispose();
                //    ///从peers字典中删除
                //    XfsTcpSession tpeer;
                //    XfsTcpServer.Instance.TPeers.TryGetValue(EcsId, out tpeer);
                //    if (tpeer != null)
                //    {
                //        //删除掉心跳包群中对应的peer
                //        XfsTcpServer.Instance.TPeers.Remove(EcsId);
                //    }
                //    Console.WriteLine(XfsTimerTool.CurrentTime() + "{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);
                //    Console.WriteLine(XfsTimerTool.CurrentTime() + " 一个客户端:已经中断连接" + " TPeers: " + XfsTcpServer.Instance.TPeers.Count);
            }
        }

    }
}