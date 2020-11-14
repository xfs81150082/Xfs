using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public static class XfsSockets
    {
        public static Dictionary<XfsSenceType, XfsTcpClient> XfsTcpClients { get; set; } = new Dictionary<XfsSenceType, XfsTcpClient>();
        public static Dictionary<XfsSenceType, XfsTcpServer> XfsTcpServers { get; set; } = new Dictionary<XfsSenceType, XfsTcpServer>();
        public static Dictionary<XfsSenceType, XfsController> XfsControllers { get; set; } = new Dictionary<XfsSenceType, XfsController>();
        public static Dictionary<XfsSenceType, XfsHandler> XfsHandlers { get; set; } = new Dictionary<XfsSenceType, XfsHandler>();
        //public static Dictionary<string, string> PeerEcids { get; set; } = new Dictionary<string, string>();




    }
}
