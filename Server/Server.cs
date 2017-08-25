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
    class Server : IWatchable
    {
        ILoggable logger;
        List<Thread> recievers = new List<Thread>();
        Thread Reciever;
        Thread Acceptor;
        private Object thiskey = new Object();
        Thread Broadcaster;
        public static Client client;
        public Dictionary<string, Client> Users;
        TcpListener server;
        Queue<string[]> queue;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse(IPFinder.GetLocalIPAddress()), 9999);//IPFinder.GetLocalIPAddress()
            Users = new Dictionary<string, Client>();
            queue = new Queue<string[]>();
            server.Start();
        }
        public Server(ILoggable logger)
        {
            this.logger = logger;
            server = new TcpListener(IPAddress.Parse(IPFinder.GetLocalIPAddress()), 9999);
            Users = new Dictionary<string, Client>();
            queue = new Queue<string[]>();
            server.Start();
        }
        public void Run()
        {
            Acceptor = new Thread(new ThreadStart(AcceptClient));
            Acceptor.Start();
            Broadcaster = new Thread(new ThreadStart(Broadcast));
            Broadcaster.Start();

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
                client = new Client(stream, clientSocket, clientNumber, this);
                string[] message = client.Recieve();
                client.Username = message[2];
                lock (thiskey)
                {
                    Users.Add(client.UserId, client);
                }
                UserUpdated();
                clientNumber++;
                Reciever = new Thread(new ThreadStart(() => CheckMessages(client)));
                recievers.Add(Reciever);
                Reciever.Start();
            }
        }
        private void Broadcast()
        {
            string[] message;
            while (true)
            {
                if (queue.Count > 0)
                {
                    message = queue.Dequeue();
                    if (message[2].StartsWith("/pm"))
                    {
                        SendToTarget(message[2], message[1]);
                        
                    }
                    else
                    {
                        PostMessage(message[1] + ": " + message[2]);
                    }
                }

            }
        }
        private void SendToTarget(string message, string sender)
        {
            int stopPoint = message.IndexOf(')');
            string user = message.Substring(4, stopPoint - 4);
            foreach (KeyValuePair<string, Client> entry in Users)
            {
                if (entry.Value.Username == sender)
                {
                    entry.Value.Send($"PM to {user}: {message.Substring(stopPoint + 1)}");
                }
                if (entry.Value.Username == user)
                {
                    user = entry.Value.UserId;
                    entry.Value.Send($"PM from {sender}: {message.Substring(stopPoint + 1)}");
                }

            }
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
        public void PostMessage(string message)
        {
            lock (thiskey)
            {

                foreach (KeyValuePair<string, Client> entry in Users)
                {
                    entry.Value.Send(message);
                }
            }
        }
        private void CheckMessages(Client client)
        {
            bool isConnected = true;
            while (isConnected)
            {
                try
                {
                    string[] message = client.Recieve();
                    queue.Enqueue(message);
                    logger.RecieveMessage($"{DateTime.Now} {client.Username}: {message[2]}" + Environment.NewLine);
                }
                catch (Exception)
                {
                    Console.WriteLine("Person left chat.");
                    lock (thiskey)
                    {
                        Users.Remove(client.UserId);
                        string[] errorMessage = new string[3] { client.UserId, client.Username, " Has been disconnected" };
                        queue.Enqueue(errorMessage);
                    }
                    UserUpdated();
                    isConnected = false;
                }
            }
        }
        public event EventHandler UsersChanged;
        public void UserUpdated()
        {

            UsersChanged(this, EventArgs.Empty);
        }

    }
}
