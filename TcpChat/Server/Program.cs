using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerListener serverListener = new ServerListener();
            serverListener.Start();

            Console.ReadLine();
            Console.ReadLine();
        }
    }
}
