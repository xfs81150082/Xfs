using System;
using System.Collections.Generic;
using System.Text;
using Xfs;

namespace XfsGateServer.Tests
{
    [XfsObjectSystem]
    class TesstClass1Awake : XfsAwakeSystem<TestClass1>
    {
        public override void Awake(TestClass1 self)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " TesstClass1Awake: " + self.test1);
        }
    }
    [XfsObjectSystem]
    class TesstClass1Update : XfsUpdateSystem<TestClass1>
    {
        public override void Update(TestClass1 self)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " TesstClass1Update: " + self.test1);
            TestTestTest().Coroutine();

        }


        async XfsVoid TestTestTest()
        {
            await new XfsSession().Call(new C4G_Ping());
        }


    }




}
