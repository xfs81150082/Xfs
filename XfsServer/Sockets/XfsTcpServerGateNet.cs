﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsServer
{
    public class XfsTcpServerGateNet : XfsNetWorkComponent
    {
        public override bool IsServer => true;
        public XfsTcpServerGateNet()
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " IsServer: " + this.IsServer);
        }

    }
}