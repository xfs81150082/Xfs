using System;
using System.Collections.Generic;
using Xfs;
using System.Text;
using System.Threading.Tasks;

namespace XfsGateServer
{
    public class TestIocp
    {
        AsyncIOCPServer server = null;
        public void Init()
        {
            server = new AsyncIOCPServer(2001, 10);
            server.Init();
            server.Start();
            Start();
            Console.ReadKey();
        }

        void Start()
        {
            new AsyncIOCPServer(2001,10).Init();
        }


    }
}
