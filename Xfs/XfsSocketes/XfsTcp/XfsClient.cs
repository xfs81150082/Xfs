using System;
//using UnityEngine;

namespace Xfs
{
    public class XfsClient : XfsTcpSession
    {
        public XfsClient()
        {
            AddComponent(new XfsClientSession());
            AddComponent(new XfsCoolDown(this.EcsId));
            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsPeer:" + this.NodeType + ":" + this.IsServer);
        }
        public XfsClient(NodeType nodeType)
        {
            this.IsServer = false;
            this.NodeType = nodeType;
            AddComponent(new XfsClientSession());
            AddComponent(new XfsCoolDown(this.EcsId));

            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsClient:" + this.NodeType + ":" + this.IsServer);
        }
        public override void OnTransferParameter(object obj, XfsParameter parameter)
        {
            ///将字符串string,用json反序列化转换成MvcParameter参数
            if (parameter.TenCode == TenCode.Zero)
            {
                this.GetComponent<XfsCoolDown>().CdCount = 0;
                return;
            }

            XfsTcpClient client = null;
            XfsSockets.XfsTcpClients.TryGetValue(this.NodeType, out client);
            if (client != null)
            {
                client.Recv(parameter);
            }
          
            Console.WriteLine(XfsTimerTool.CurrentTime() + " 157 XfsTcpSession is Client");
       
        }
        public override void OnConnect()
        {
            ///显示与客户端连接
            Console.WriteLine("{0} 服务端{1}连接成功", XfsTimerTool.CurrentTime(), Socket.RemoteEndPoint);
        }///与服务器连接时调用  
        public override void XfsDispose()
        {
            XfsTcpClient client = null;
            XfsSockets.XfsTcpClients.TryGetValue(this.NodeType, out client);
            if (client != null)
            {
                base.XfsDispose();
                if (client.TClient != null && client.TClient.EcsId == this.EcsId)
                {
                    client.TClient = null;
                }
                ///设置连接中断，40秒后会自动重连
                client.IsRunning = false;
                Console.WriteLine("{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);

            }


            //if (XfsTcpClient.Instance.NodeType == this.NodeType)
            //{
            //    base.XfsDispose();
            //    if (XfsTcpClient.Instance.TClient != null && XfsTcpClient.Instance.TClient.EcsId == this.EcsId)
            //    {
            //        XfsTcpClient.Instance.TClient = null;
            //    }
            //    ///设置连接中断，40秒后会自动重连
            //    XfsTcpClient.Instance.IsRunning = false;
            //    Console.WriteLine("{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);
            //}


            //if (XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>() != null)
            //{
            //    if (XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().TClient != null && XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().TClient.EcsId == this.EcsId)
            //    {
            //        XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().TClient = null;
            //    }
            //    ///设置连接中断，40秒后会自动重连
            //    XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().IsRunning = false;
            //}

        }///与服务器断开时调用                      
    }
}