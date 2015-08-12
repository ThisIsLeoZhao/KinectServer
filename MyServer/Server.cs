
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using Newtonsoft.Json;
using SingleKinect.EngagementManager;

namespace MyServer
{
    public class Account
    {
        public bool hasReceived;
        public DateTime CreatedDate { get; set; }
    }

    public class Server
    {
        private static TcpListener listener;
        private static TcpClient client;
        internal static int clientID = 0;

        public static void start()
        {
            //IPAddress localAdd = IPAddress.Parse("0.0.0.0");
            listener = new TcpListener(5000);
            Console.WriteLine("Listening...");
            listener.Start();
                        
            while (true)
            {
                //---incoming client connected---
                client = listener.AcceptTcpClient();
                            
                HandleClient clientHandler = new HandleClient(client);
                ThreadPool.QueueUserWorkItem(clientHandler.doChat, clientID);
            
                Console.WriteLine("client {0} on board", clientID);
                clientID++;
            }
                        
            close();
            
    
        
//            while (true)
//            {
//                Console.WriteLine("Hello World!!! " + DateTime.UtcNow);
//                Thread.Sleep(1000);
//            }
            
        }
    


        private static void close()
        {
            
            listener.Stop();
        }
    }

    
}