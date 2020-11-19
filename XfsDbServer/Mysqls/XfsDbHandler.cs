using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsDbServer
{
    public class XfsDbHandler : XfsHandler
    {
        public override XfsSenceType SenceType => XfsSenceType.Db;
        public XfsDbHandler()
        {
        }
        public override void Recv(object obj, XfsParameter parameter)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                   

                    break;
                case (TenCode.Code0002):
                  

                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }

        //XfsTcpServerDbNet DbServer()
        //{
        //    XfsTcpServer ser = null;
        //    XfsSockets.XfsTcpServers.TryGetValue(this.SenceType, out ser);
        //    if (ser != null)
        //    {
        //        return ser as XfsTcpServerDbNet;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}



    }
}