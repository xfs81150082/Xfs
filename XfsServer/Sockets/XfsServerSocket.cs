using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Xfs;

namespace XfsServer
{
    public class XfsServerSocket : XfsSystem
    {
        public XfsTcpServer TcpServer = null;
        public XfsServerSocket()
        {
            TcpServer = new XfsTcpServer();
        }
        public XfsServerSocket(string ipString, int port, int maxListenCount)
        {
            TcpServer = new XfsTcpServer();
            TcpServer.Init(ipString, port, maxListenCount);
        }

        public override void XfsUpdate()
        {
            ServerStart();
            ServerRecvParameters();
        }
        void ServerStart()
        {
            if (!TcpServer.IsRunning)
            {
                TcpServer.Init("127.0.0.1", 8115, 10);
                //TcpServer.Init("172.17.16.15", 8115, 10);
                TcpServer.StartListen();
            }
        }
        public void ServerRecvParameters()
        {
            try
            {
                while (TcpServer.RecvParameters.Count > 0)
                {
                    XfsParameter parameter = TcpServer.RecvParameters.Dequeue();
                    //if (TmGateHandler.Instance != null)
                    //{
                    //    TmGateHandler.Instance.OnTransferParameter(this, parameter);
                    //    Console.WriteLine(TmTimerTool.CurrentTime() + " RecvParameters: " + TcpServer.RecvParameters.Count);
                    //}
                    //else
                    //{
                    //    Console.WriteLine(TmTimerTool.CurrentTime() + " TumoGate is null.");
                    //    break;
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + ex.Message);
            }
        }
    }
}