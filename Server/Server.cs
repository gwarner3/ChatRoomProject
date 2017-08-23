using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Client;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        Thread Reciever;
        Thread Acceptor;
        public static Client client;
        TcpListener server;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse(IPFinder.GetLocalIPAddress()), 9999);
            server.Start();
        }
        public void Run()
        {
            Acceptor = new Thread(new ThreadStart(AcceptClient));
            Acceptor.Start();
            //CheckMessages();


        }

        private void AcceptClient()
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
 
            }
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
        //private async Task CheckMessages()
        //{
        //    await MessageRecieved();
        //}
        //private Task<int> MessageRecieved()
        //{
        //    client.stream.Read(client.recievedMessage, 0, client.recievedMessage.Length);
        //    return 50;
        //}
    }
}
