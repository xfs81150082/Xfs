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
            TestCall(self);
        }

        int time = 0;
        int restime = 3000;
        async void TestCall2(XfsTest self)
        {
            time += 1;
            if (time > restime)
            {
                time = 0;

                string tt = self.call;

                C2G_TestRequest c2G_Test = new C2G_TestRequest();
                c2G_Test.Message = self.call;                    
                c2G_Test.ActorId = 1100;

                XfsMessageInfo messageInfo = new XfsMessageInfo();
                messageInfo.Opcode = XfsOuterOpcode.C2G_TestRequest;
                messageInfo.Message = c2G_Test;

                XfsTcpClient client = null;
                XfsSockets.XfsTcpClients.TryGetValue(XfsSenceType.Client, out client);
                if (client != null && client.TClient != null)
                {
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-54,开始打电话给服务器。 ");

                    XfsMessageInfo response = await client.TClient.Call(messageInfo);

                    string res = ((G2C_TestResponse)response.Message).Message;

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-59: " + res);
                }

                //Console.WriteLine(XfsTimerTool.CurrentTime() + " 55 XfsTestSystem: " + test.time);
            }
        }

        async void TestCall(XfsTest self)
        {
            time += 1;
            if (time > restime)
            {
                time = 0;

                string tt = self.call;
                XfsParameter request = XfsParameterTool.ToParameter(TenCode.Code0004, ElevenCode.Code0004, ElevenCode.Code0004.ToString(), tt);
                request.Back = true;
                request.EcsId = XfsIdGeneraterHelper.GetId();

                XfsTcpClient client = null;
                XfsSockets.XfsTcpClients.TryGetValue(XfsSenceType.Client, out client);
                if (client != null && client.TClient != null)
                {
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-54,开始打电话给服务器。 ");

                    XfsParameter response = await client.TClient.Call(request);

                    string res = XfsParameterTool.GetValue<string>(response, response.ElevenCode.ToString());

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-59: " + res);
                }

                //Console.WriteLine(XfsTimerTool.CurrentTime() + " 55 XfsTestSystem: " + test.time);
            }
        }





    }
}
