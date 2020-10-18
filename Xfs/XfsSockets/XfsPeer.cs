using System;
using System.Net.Sockets;
namespace Xfs
{
    public class XfsPeer : XfsTcpSession
    {
        public XfsPeer()
        {
            IsServer = true;
        }
        public override void OnConnect()
        {
            ///显示与客户端连接
            Console.WriteLine("{0} 客户端{1}连接成功", XfsTimerTool.CurrentTime() , Socket.RemoteEndPoint);
            XfsTcpSession tpeer = null;
            bool yes1 = XfsGame.XfsSence.GetComponent<XfsTcpServer>().TPeers.TryGetValue(this.EcsId, out tpeer);
            if (yes1 != true)
            {
                ///tpeers已经加入字典
                XfsGame.XfsSence.GetComponent<XfsTcpServer>().TPeers.Add(this.EcsId, this);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ComponentId: " + this.EcsId + " 已经加入字典");
            }
            ///显示客户端群中的客户端数量
            Console.WriteLine(XfsTimerTool.CurrentTime() + " TPeers Count: " + XfsGame.XfsSence.GetComponent<XfsTcpServer>().TPeers.Count);
        }
        public override void XfsDispose()
        {
            base.XfsDispose();
            ///从peers字典中删除
            XfsTcpSession tpeer;
            XfsGame.XfsSence.GetComponent<XfsTcpServer>().TPeers.TryGetValue(EcsId, out tpeer);
            if (tpeer != null)
            {
                //删除掉心跳包群中对应的peer
                XfsGame.XfsSence.GetComponent<XfsTcpServer>().TPeers.Remove(EcsId);
            }           
            Console.WriteLine(XfsTimerTool.CurrentTime() + "{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);
            Console.WriteLine(XfsTimerTool.CurrentTime() + " 一个客户端:已经中断连接" + " TPeers: " + XfsGame.XfsSence.GetComponent<XfsTcpServer>().TPeers.Count);
        }

    }
}