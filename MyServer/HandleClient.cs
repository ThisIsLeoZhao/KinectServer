using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MyServer
{
    public class HandleClient
    {
        private TcpClient client;
        private static Hashtable clientTable = new Hashtable();

        public HandleClient(TcpClient tcpClient)
        {
            this.client = tcpClient;
            clientTable.Add(client, Server.clientID);
        }

        public void doChat(Object threadContext)
        {
            while (true)
            {
                try
                {
                    NetworkStream nwStream = client.GetStream();

                    StreamWriter writer = new StreamWriter(nwStream, Encoding.ASCII) { AutoFlush = true };
                    StreamReader reader = new StreamReader(nwStream, Encoding.ASCII);

                    string inputLine = reader.ReadLine();

                    Console.WriteLine(inputLine);

                    string outputLine = "Echo: " + inputLine;
                    writer.WriteLine(outputLine);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex);
                    client.Close();
                    Console.WriteLine("Client {0} exit.", clientTable[client]);
                    int a;
                    int b;
                    ThreadPool.GetAvailableThreads(out a, out b);
                    Console.WriteLine("{0}, {1}", a, b);
                    return;
                }

            }



        }
    }
}