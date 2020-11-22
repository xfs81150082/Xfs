using System;
using System.Collections.Generic;
using System.Text;
using Xfs;

namespace XfsGateServer
{
    public class TestNanample : XfsEntity
    {
        public TestNanample(){}
        public void Start()
        {
            this.AddComponent<XfsGateTest>();                                     ///服务器加载组件 : 通信组件Server
            this.AddComponent<TestClass1>();                                     ///服务器加载组件 : 通信组件Server



        }


    }

}
