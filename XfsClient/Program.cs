using System;
using Xfs;

namespace XfsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new XfsClientInit().Start();

            Console.ReadLine();

        }
    }
}
