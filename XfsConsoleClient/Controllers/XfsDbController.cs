using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
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
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + "18 XfsControllers: " + tenCode);
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + "19 XfsControllers: " + parameter.ElevenCode);

                    string va = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());
                    XfsModelObjects.Tests.Add(va);


                    Console.WriteLine(XfsTimeHelper.CurrentTime() + "23 XfsHandlers: " + "" + va);

                    break;
                case (TenCode.Code0002):
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsControllers: " + tenCode);

                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }
    }
}
