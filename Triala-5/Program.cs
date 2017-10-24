using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triala_5
{
    class Program
    {
        private const int PORT = 9999;

        static void Main(string[] args)
        {
            Server server = new Server(PORT);
            server.Start();

            Console.ReadLine();
        }
    }
}
