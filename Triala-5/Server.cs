using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;

namespace Triala_5
{
    internal class Server
    {
        private readonly int PORT;

        public Server(int port)
        {
            this.PORT = port;
        }

        public void Start()
        {
            
            TcpListener listener = TcpListener.Create(PORT);
            listener.Start();

            while (true)
            {
                //using (TcpClient client = listener.AcceptTcpClient())
                {
                    // overvej om seperat tråd (Task.Run( () => DoIt(client))
                    TcpClient client = listener.AcceptTcpClient();
                    Task.Run( () =>DoIt(client));

                }
            }

        }

        private void DoIt(TcpClient client)
        {
            
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // read
                String str = reader.ReadLine();


                //process
                string[] strings = str.Split(' ');

                // evt fejl behandling fx. strings.Length == 4

                double val1 = double.Parse(strings[1]);
                double val2 = double.Parse(strings[2]);
                double val3 = double.Parse(strings[3]);

                // write
                if (strings[0].ToLower() == "volume")
                {
                    writer.WriteLine(val1*val2*val3);
                    writer.Flush();
                }

                if (strings[0].ToLower() == "side")
                {

                    // fejl behandling val2 eller val3 == 0 => exception 
                    writer.WriteLine(val1 / (val2 * val3) );
                    writer.Flush();
                }

                // hvis rigtigt med TASK
                client.Close();

            }
        }
    }
}