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
        //public static Dictionary<string, string> PeerEcids { get; set; } = new Dictionary<string, string>();




    }
}
