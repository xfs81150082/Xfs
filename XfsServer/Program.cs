using System;
using Xfs;

namespace XfsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new XfsGateInit().Start();

            Console.ReadLine();

        }
    }
}
