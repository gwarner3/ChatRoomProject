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
        List<Thread> recievers = new List<Thread>();
        Thread Reciever;
        Thread Acceptor;
        public static Client client;
        public Dictionary<string, Client> Users;
        TcpListener server;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse(IPFinder.GetLocalIPAddress()), 9999);
            Users = new Dictionary<string, Client>();
            server.Start();
        }
        public void Run()
        {
            Acceptor = new Thread(new ThreadStart(AcceptClient));
            Acceptor.Start();



        }

        private void AcceptClient()
        {
            int clientNumber;
            clientNumber = 0;
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket, clientNumber);
                client.Username = client.Recieve();

                Users.Add(client.UserId, client);
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
            foreach (KeyValuePair<string, Client> entry in Users)
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
                    PostMessage($"{client.Username}: {message}");
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
