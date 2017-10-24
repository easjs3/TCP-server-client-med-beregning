using System;
using System.IO;
using System.Net.Sockets;
using System.Xml;

namespace Client
{
    internal class ClientClass
    {
        private readonly int PORT;

        public ClientClass(int port)
        {
            this.PORT = port;
        }

        public void Start()
        {
            string msg = "Volume 2 4.5 6";
            SendRequest( msg);

            String msg2 = "Side 540 4.5 6";
            SendRequest(msg2);
        }

        private void SendRequest( string msg)
        {
            using (TcpClient client = new TcpClient("localhost", PORT))
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(msg);
                writer.Flush();

                String resStr = reader.ReadLine();
                Console.WriteLine($"Resultatet af {msg} er {resStr}");

                
            }
        }
    }
}