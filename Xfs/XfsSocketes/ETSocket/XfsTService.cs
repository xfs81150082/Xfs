using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsTService : XfsAService
    {
        public object MemoryStreamManager { get; internal set; }
        public int PacketSizeLength { get; internal set; }

        public override XfsAChannel ConnectChannel(IPEndPoint ipEndPoint)
        {
            throw new NotImplementedException();
        }

        public override XfsAChannel ConnectChannel(string address)
        {
            throw new NotImplementedException();
        }

        public override XfsAChannel GetChannel(long id)
        {
            throw new NotImplementedException();
        }

        public override void Remove(long channelId)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
