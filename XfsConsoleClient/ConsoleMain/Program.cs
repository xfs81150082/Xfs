using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XfsConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            new XfsClientInit().Start();

            Console.ReadLine();

        }
    }
}
