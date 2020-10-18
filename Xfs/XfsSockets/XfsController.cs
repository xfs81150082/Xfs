﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public abstract class XfsController : XfsComponent
    {
        private static XfsController _instance;
        public static XfsController Instance { get => _instance; }
        public XfsController() { _instance = this; }
        public abstract void Recv(XfsParameter parameter);
    }
}