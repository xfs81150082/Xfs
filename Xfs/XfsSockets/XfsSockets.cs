using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public static class XfsSockets
    {
        private static Dictionary<NodeType, XfsTcpClient> XfsTcpClients = new Dictionary<NodeType, XfsTcpClient>();
        private static Dictionary<NodeType, XfsTcpServer> XfsTcpServers = new Dictionary<NodeType, XfsTcpServer>();
        private static Dictionary<NodeType, XfsController> XfsControllers = new Dictionary<NodeType, XfsController>();
        private static Dictionary<NodeType, XfsHandler> XfsHandlers = new Dictionary<NodeType, XfsHandler>();

        public static void AddXfsController(XfsController tcpClient)
        {
            XfsController client = null;
            XfsControllers.TryGetValue(tcpClient.NodeType, out client);
            if (client != null)
            {
                XfsControllers.Add(tcpClient.NodeType, tcpClient);
            }
            else
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpClient 已存在:" + tcpClient.NodeType);
            }


        }
        public static XfsController GetXfsController(NodeType nodeType)
        {
            XfsController client = null;
            XfsControllers.TryGetValue(nodeType, out client);
            if (client != null)
            {
                return client;
            }
            return null;
        }
        public static XfsHandler GetXfsHandler(NodeType nodeType)
        {
            XfsHandler server = null;
            XfsHandlers.TryGetValue(nodeType, out server);
            if (server != null)
            {
                return server;
            }
            return null;
        }
        public static void AddXfsHandler(XfsHandler tcpServer)
        {
            XfsHandler server = null;
            XfsHandlers.TryGetValue(tcpServer.NodeType, out server);
            if (server == null)
            {
                XfsHandlers.Add(tcpServer.NodeType, tcpServer);
            }
            else
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpServer 已存在:" + tcpServer.NodeType);
            }
        }


        public static void AddTcpClient(XfsTcpClient tcpClient)
        {
            XfsTcpClient client = null;
            XfsTcpClients.TryGetValue(tcpClient.NodeType, out client);
            if (client == null)
            {
                XfsTcpClients.Add(tcpClient.NodeType, tcpClient);
            }
            else
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpClient 已存在:" + tcpClient.NodeType);
            }
        }
        public static XfsTcpClient GetTcpClient(NodeType nodeType)
        {
            XfsTcpClient client = null;
            XfsTcpClients.TryGetValue(nodeType, out client);
            if (client != null)
            {
                return client;
            }
            return null;
        }
        public static void AddTcpServer(XfsTcpServer tcpServer)
        {
            XfsTcpServer server = null;
            XfsTcpServers.TryGetValue(tcpServer.NodeType, out server);
            if (server == null)
            {
                XfsTcpServers.Add(tcpServer.NodeType, tcpServer);
            }
            else
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpServer 已存在:" + tcpServer.NodeType);
            }
        }
        public static XfsTcpServer GetTcpServer(NodeType nodeType)
        {
            XfsTcpServer server = null;
            XfsTcpServers.TryGetValue(nodeType, out server);
            if (server != null)
            {
                return server;
            }
            return null;
        }



    }
}
