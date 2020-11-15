using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsNetWorkComponent : XfsComponent
    {
        public Dictionary<XfsSenceType, XfsTcpClient> SenceClients { get; set; } = new Dictionary<XfsSenceType, XfsTcpClient>();
        public Dictionary<XfsSenceType, XfsTcpServer> SenceServers { get; set; } = new Dictionary<XfsSenceType, XfsTcpServer>();
        public Dictionary<IPAddress, XfsTcpClient> IPAddressClients { get; set; } = new Dictionary<IPAddress, XfsTcpClient>();
        public Dictionary<IPAddress, XfsTcpServer> IPAddressServers { get; set; } = new Dictionary<IPAddress, XfsTcpServer>();
        public XfsDoubleMap<IPAddress, long> ClientIpIds { get; set; } = new XfsDoubleMap<IPAddress, long>();
        public XfsDoubleMap<IPAddress, long> ServerIpIds { get; set; } = new XfsDoubleMap<IPAddress, long>();
    }

    public class XfsNetWorkComponentLoadSystem : XfsLoadSystem<XfsComponent>
    {
        public override void Load(XfsComponent self)
        {
            TcpLoad(self);
        }

        void TcpLoad(XfsComponent self)
        {






        }
    }

}