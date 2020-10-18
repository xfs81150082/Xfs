using System;
using System.Collections.Generic;
using System.Threading;
using Xfs;

namespace XfsServer
{
    class XfsServerMain
    { 
        //程序启动入口
        static void Main(string[] args)
        {
            new XfsServerInit().ConsoleInit();  
        }
        

    }
}