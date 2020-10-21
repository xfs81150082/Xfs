using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public static class XfsSockets
    {
        public static Dictionary<NodeType, XfsTcpClient> XfsTcpClients { get; set; } = new Dictionary<NodeType, XfsTcpClient>();
        public static Dictionary<NodeType, XfsTcpServer> XfsTcpServers { get; set; } = new Dictionary<NodeType, XfsTcpServer>();
        public static Dictionary<NodeType, XfsController> XfsControllers { get; set; } = new Dictionary<NodeType, XfsController>();
        public static Dictionary<NodeType, XfsHandler> XfsHandlers { get; set; } = new Dictionary<NodeType, XfsHandler>();

        //public static void AddXfsController(NodeType nodeType,XfsController tcpClient)
        //{
        //    XfsController client = null;
        //    XfsControllers.TryGetValue(nodeType, out client);
        //    if (client == null)
        //    {
        //        XfsControllers.Add(nodeType, tcpClient);
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsController 已注册:" + nodeType + " Cout:" + XfsControllers.Count);
        //    }
        //    else
        //    {
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsController 已存在:" + nodeType);
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsControllers Cout:" + XfsControllers.Count);
        //    }


        //}
        //public static XfsController GetXfsController(NodeType nodeType)
        //{
        //    XfsController client = null;
        //    XfsControllers.TryGetValue(nodeType, out client);
        //    if (client == null)
        //    {
        //        return null;
        //    }
        //    return client;
        //}
        //public static void AddXfsHandler(NodeType nodeType, XfsHandler tcpServer)
        //{
        //    XfsHandler server = null;
        //    XfsHandlers.TryGetValue(nodeType, out server);
        //    if (server == null)
        //    {
        //        XfsHandlers.Add(nodeType, tcpServer);
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandler 已注册:" + nodeType);
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandlers Cout:" + XfsHandlers.Count);
        //    }
        //    else
        //    {
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandler 已存在:" + nodeType);
        //    }
        //}
        //public static XfsHandler GetXfsHandler(NodeType nodeType)
        //{
        //    XfsHandler client = null;
        //    XfsHandlers.TryGetValue(nodeType, out client);
        //    if (client == null)
        //    {
        //        return null;
        //    }
        //    return client;
        //}
        //public static void AddTcpClient(NodeType nodeType, XfsTcpClient tcpClient)
        //{
        //    XfsTcpClient client = null;
        //    XfsTcpClients.TryGetValue(nodeType, out client);
        //    if (client == null)
        //    {
        //        XfsTcpClients.Add(nodeType, tcpClient);
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpClient 已注册:" + nodeType);
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpClients Cout:" + XfsTcpClients.Count);
        //    }
        //    else
        //    {
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpClient 已存在:" + nodeType);
        //    }
        //}
        //public static XfsTcpClient GetTcpClient(NodeType nodeType)
        //{
        //    XfsTcpClient client = null;
        //    XfsTcpClients.TryGetValue(nodeType, out client);
        //    if (client == null)
        //    {
        //        return null;
        //    }
        //    return client;
        //}
        //public static void AddTcpServer(NodeType nodeType, XfsTcpServer tcpServer)
        //{
        //    XfsTcpServer server = null;
        //    XfsTcpServers.TryGetValue(nodeType, out server);
        //    if (server == null)
        //    {
        //        XfsTcpServers.Add(nodeType, tcpServer);
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpServer 已注册:" + nodeType);
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpServers Cout:" + XfsTcpServers.Count);
        //    }
        //    else
        //    {
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTcpServer 已存在:" + nodeType);
        //    }
        //}
        //public static XfsTcpServer GetTcpServer(NodeType nodeType)
        //{
        //    XfsTcpServer server = null;
        //    XfsTcpServers.TryGetValue(nodeType, out server);
        //    if (server == null)
        //    {
        //        return null;
        //    }
        //    return server;
        //}



    }
}
