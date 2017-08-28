using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Server
{
    class Server : IWatchable
    {
        ILoggable logger;
        TcpListener server;
        Thread Reciever;
        Thread Acceptor;
        Thread Broadcaster;
        Queue<string[]> queue;
        public Client client;
        public Dictionary<string, Client> Users;
        private object thiskey;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse(IPFinder.GetLocalIPAddress()), 9999); //IPFinder.GetLocalIPAddress()
            Users = new Dictionary<string, Client>();
            queue = new Queue<string[]>();
            thiskey = new object();
            server.Start();
        }
        public Server(ILoggable logger)
        {
            this.logger = logger;
            server = new TcpListener(IPAddress.Parse(IPFinder.GetLocalIPAddress()), 9999);
            Users = new Dictionary<string, Client>();
            queue = new Queue<string[]>();
            thiskey = new object();
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
                TcpClient clientSocket;
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket, clientNumber, this);
                SetClientUserName(client);
                clientNumber++;
                Reciever = new Thread(new ThreadStart(() => CheckMessages(client)));
                Reciever.Start();
            }
        }
        private void SetClientUserName(Client client)
        {
            string[] message = client.Recieve();
            client.Username = message[2];
            lock (thiskey)
            {
                Users.Add(client.UserId, client);
            }
            UserUpdated();
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
            lock (thiskey)
            {
              foreach (KeyValuePair<string, Client> entry in Users)
            {
                if (entry.Value.Username == sender)
                {
                    entry.Value.Send($"PM to {user}: {message.Substring(stopPoint + 1)}");
                }
                if (entry.Value.Username == user)
                {
                    entry.Value.Send($"PM from {sender}: {message.Substring(stopPoint + 1)}");
                }
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
                    RemoveUser(client);
                    isConnected = false;
                }
            }
        }
        private void RemoveUser(Client client)
        {
            lock (thiskey)
            {
                Users.Remove(client.UserId);
                string[] errorMessage = new string[3] { client.UserId, client.Username, " has been disconnected" };
                queue.Enqueue(errorMessage);
            }
            UserUpdated();
        }
        public event EventHandler UsersChanged;
        public void UserUpdated()
        {

            UsersChanged(this, EventArgs.Empty);
        }

    }
}
