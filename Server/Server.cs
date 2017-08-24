using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        List<Thread> recievers = new List<Thread>();
        Thread Reciever;
        Thread Acceptor;
        public static Client client;
        List<Client> Users;
        TcpListener server;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse(IPFinder.GetLocalIPAddress()), 9999);
            Users = new List<Client>();
            server.Start();
        }
        public void Run()
        {
            Acceptor = new Thread(new ThreadStart(AcceptClient));
            Acceptor.Start();



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
                client.UserId = client.Recieve();
                Users.Add(client);
                Reciever = new Thread(new ThreadStart(() => CheckMessages(client)));
                recievers.Add(Reciever);
                Reciever.Start();

            }
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
        public void PostMessage(string message)
        {
            foreach (Client client in Users)
            {
                client.Send(message);
            }
        }
        private void CheckMessages(Client client)
        {
            bool isConnected = true;
            while (isConnected)
            {
                try
                {
                    string message = client.Recieve();
                    PostMessage($"{client.UserId}: {message}");
                }
                catch (Exception)
                {
                    Console.WriteLine("Person left chat.");
                    isConnected = false;
                }
            }
        }
        //private Task<int> MessageRecieved()
        //{
        //    client.stream.Read(client.recievedMessage, 0, client.recievedMessage.Length);
           
        //}
    }
}
