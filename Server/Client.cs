using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Client : IWatcher
    {
        public byte[] recievedMessage;
        public NetworkStream stream;
        TcpClient client;
        Server server;
        public string UserId;
        public string Username;
        public Client(NetworkStream Stream, TcpClient Client, int number, Server server)
        {
            this.server = server;
            server.UsersChanged += Server_UsersChanged;
            stream = Stream;
            client = Client;
            UserId = number.ToString();
        }

        private void Server_UsersChanged(object sender, EventArgs e)
        {
            List<string> UserNames = new List<string>();
            string[] names;
            string usernames;
            foreach (KeyValuePair<string, Client> entry in server.Users)
            {
                UserNames.Add(entry.Value.Username);
            }
            names = UserNames.ToArray();
            usernames = string.Join(";", names);
            Update(usernames);  
         }
        public void Send(string Message)
        {
            byte[] message = Encoding.ASCII.GetBytes(Message);
            stream.Write(message, 0, message.Count());
        }
        public string[] Recieve()
        {
            string[] recievedMessageContent = new string[3];
            recievedMessage = new byte[256];
            Console.WriteLine("message recieved");
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            recievedMessage = CleanMessage(recievedMessage);
            recievedMessageContent[2] = Encoding.ASCII.GetString(recievedMessage);
            recievedMessageContent[1] = Username;
            recievedMessageContent[0] = UserId;
            return recievedMessageContent;

        }
        private byte[] CleanMessage(byte[] message)
        {
            int i = message.Length - 1;
            while (message[i] == 0)
            {
                --i;
            }
            byte[] CleanMessage = new byte[i + 1];
            Array.Copy(message, CleanMessage, i + 1);
            return CleanMessage;
        }

        public void Update(string name)
        {

                try
                {
                    byte[] message = Encoding.ASCII.GetBytes("/<>"+name);
                    stream.Write(message, 0, message.Count());
                }
                catch
                {
                    Thread.CurrentThread.Abort();
                }
        }
    }
}
