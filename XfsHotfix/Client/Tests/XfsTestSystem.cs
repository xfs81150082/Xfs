using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
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

                C2G_TestRequest resqustC = new C2G_TestRequest();
                resqustC.Opcode = XfsGame.Scene.GetComponent<XfsOpcodeTypeComponent>().GetOpcode(resqustC.GetType());
                resqustC.Message = self.call;

                //IPEndPoint point = XfsNetworkHelper.ToIPEndPoint("127.0.0.1:2001");
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-42,开始打电话给服务器...");

                XfsSession session = XfsGame.Scene.GetComponent<XfsNetOuterComponent>().Session;

                if (session != null && session.IsRunning)
                {
                    G2C_TestResponse responseC = (G2C_TestResponse)await session.Call(resqustC);
                    string mes = responseC.Message;

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-43: " + mes);
                }
                else
                {
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-48: session is null");
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
