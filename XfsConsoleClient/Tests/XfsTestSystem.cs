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
        int restime = 8000;

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

                XfsTcpClient client = XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>();

                if (client != null && client.TClient != null && client.IsRunning)
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
