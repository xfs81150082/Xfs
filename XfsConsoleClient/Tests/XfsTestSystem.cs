using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    [XfsObjectSystem]    
    public class XfsTestSystem : XfsUpdateSystem<XfsTest>
    {
        public override void Update(XfsTest self)
        {
            TestCall3(self);

        }

        async void TestCall3(XfsTest self)
        {
            self.time += 1;
            if (self.time > self.restime)
            {
                self.time = 0;

                C4G_Ping resqustC = new C4G_Ping();
                resqustC.Opcode = XfsGame.XfsSence.GetComponent<XfsOpcodeTypeComponent>().GetOpcode(resqustC.GetType());
                resqustC.Message = self.call;

                XfsTcpClient client = XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>();
                if (client != null && client.TClient != null && client.IsRunning)
                {
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-42,开始打电话给服务器...");

                    G4C_Ping responseC = (G4C_Ping)await client.TClient.Call(resqustC);
                    string mes = responseC.Message;

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-48: " + mes);
                }

            }
        }
    }

    public class XfsTest : XfsEntity
    {
        public float time = 0;

        public float restime = 8000;

        public string call = "从客户端发送到服务器...";
    }

}
