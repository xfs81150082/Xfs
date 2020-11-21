using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    [XfsObjectSystem]
     public class XfsGateTestSystem : XfsUpdateSystem<XfsGateTest>
    {
        public override void Update(XfsGateTest self)
        {
            //TestCall2(self);

            //TestCall3(self);


        }

        int time = 0;
        int restime = 4000;

        void TestCall3(XfsGateTest self)
        {
            time += 1;
            if (time > restime)
            {
                time = 0;
                ///*XfsMessageHandlerComponent*/ xfsMessage = XfsGame.XfsSence.GetComponent<XfsMessageHandlerComponent>();

                //Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsGateTestSystem-35: " + xfsMessage.Handlers.Count);
                //Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsGateTestSystem-36: " + xfsMessage.Handlers.Values);
              
                
                //for (int i = 0; i < xfsMessage.Handlers.Count; i++)
                //{
                //    //Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsGateTestSystem-44: " + xfsMessage.Handlers.Keys.ToList<ushort>()[i] + " : " + xfsMessage.Handlers[xfsMessage.Handlers.Keys.ToList<ushort>()[i]]);
                //}

                //ushort opcode1 = 222;
                //Type messageType = XfsGame.XfsSence.GetComponent<XfsOpcodeTypeComponent>().GetType(opcode1);
                //int opcode2 = XfsGame.XfsSence.GetComponent<XfsOpcodeTypeComponent>().GetOpcode(messageType);

                //Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsGateTestSystem-47: " + opcode1 + " : " + messageType + " : " + opcode2);


                //XfsOpcodeTypeComponent xfsOpcode = XfsGame.XfsSence.GetComponent<XfsOpcodeTypeComponent>();
                //Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: " + xfsOpcode.MessagesCount());
                //Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-39: " + xfsOpcode.Keys());
                //Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-40: " + xfsOpcode.Messages());

                //for (int i = 0; i < xfsOpcode.MessagesCount(); i++)
                //{
                //    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-44: " + xfsOpcode.Keys()[0] + " : " + xfsOpcode.GetType(xfsOpcode.Keys()[0]));
                //}

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-47: ");

            }
        }
        async void TestCall2(XfsGateTest self)
        {
            time += 1;
            if (time > restime)
            {
                time = 0;
                XfsOpcodeTypeComponent xfsOpcode = XfsGame.XfsSence.GetComponent<XfsOpcodeTypeComponent>();
                XfsMessageDispatcherComponent xfsMessage = XfsGame.XfsSence.GetComponent<XfsMessageDispatcherComponent>();

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: " + xfsMessage.Handlers.Count);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: " + xfsMessage.Handlers.Values);

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: " + xfsOpcode.MessagesCount());
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: " + xfsOpcode.Keys());
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: " + xfsOpcode.Messages());






                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: ");

                await new XfsTask();
            }
        }




    }


    public class XfsGateTest : XfsEntity
    {
        public string longin = "2020-10-18";
        public string par { get; set; }

        public bool IsUserLogin = false;

        public float time = 0;

        public float restime = 3000;

        public string call = "发送测试回调请求-20201106";

    }

}
