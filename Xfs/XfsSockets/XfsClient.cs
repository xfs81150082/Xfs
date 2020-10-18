using System;
//using UnityEngine;

namespace Xfs
{
    public class XfsClient : XfsTcpSession
    {
        public XfsClient()
        {
            XfsGame.XfsSence.GetComponent<XfsTcpClient>().TClient = this;
            this.IsServer = false;
        }
        public override void OnConnect()
        {
            ///显示与客户端连接
            Console.WriteLine("{0} 服务端{1}连接成功", XfsTimerTool.CurrentTime(), Socket.RemoteEndPoint);
        }///与服务器连接时调用  
        public override void XfsDispose()
        {
            base.XfsDispose();
            if (XfsGame.XfsSence.GetComponent<XfsTcpClient>() != null)
            {
                if (XfsGame.XfsSence.GetComponent<XfsTcpClient>().TClient != null && XfsGame.XfsSence.GetComponent<XfsTcpClient>().TClient.EcsId == this.EcsId)
                {
                    XfsGame.XfsSence.GetComponent<XfsTcpClient>().TClient = null;
                }
                ///设置连接中断，40秒后会自动重连
                XfsGame.XfsSence.GetComponent<XfsTcpClient>().IsRunning = false;
            }

            Console.WriteLine("{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);
        }///与服务器断开时调用                      
    }
}