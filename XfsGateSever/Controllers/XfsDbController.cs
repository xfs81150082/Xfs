using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class XfsDbController : XfsController
    {
        public override XfsSenceType SenceType => XfsSenceType.Db;

        public override void Recv(object obj, XfsParameter parameter)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                    string va = XfsMessageHelper.GetValue<string>(parameter);
                    XfsModelObjects.Tests.Add(va);


                    Console.WriteLine(XfsTimeHelper.CurrentTime() + "23 XfsHandlers: " + "" + va);

                    break;
                case (TenCode.Code0002):
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsControllers: " + tenCode);

                    break;
                case (TenCode.Code0003):                  
                    string va3 = XfsMessageHelper.GetValue<string>(parameter);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsDbController，Node已收到回复信息: " + va3);
                    //Console.WriteLine(XfsTimeHelper.CurrentTime() + " parameter.PeerIds: " + parameter.PeerIds.Count);

                    //XfsGame.XfsSence.GetComponent<XfsTcpServerNodeNet>().Send(parameter);

                    //XfsTcpServer server3 = null;
                    //XfsSockets.XfsTcpServers.TryGetValue(XfsSenceType.Db, out server3);
                    //if (server3 != null)
                    //{
                    //    //(server3 as XfsTcpServerNodeNet).Send(parameter);
                    //}
                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }
    }
}
