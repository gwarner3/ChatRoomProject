using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Client : IWatcher
    {
        public byte[] recievedMessage;
        public string UserId;
        public string Username;
        NetworkStream stream;
        TcpClient client;
        Server server;
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
            List<string> UserNames;
            string[] names;
            UserNames = new List<string>();
            foreach (KeyValuePair<string, Client> entry in server.Users)
            {
                UserNames.Add(entry.Value.Username);
            }
            names = UserNames.ToArray();
            PackageNamesForMessage(names);
         }
        private void PackageNamesForMessage(string[] names)
        {
            string usernames;
            usernames = string.Join(";", names);
            Update(usernames);
        }
        public void Send(string Message)
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(Message);
                stream.Write(message, 0, message.Count());
            }
            catch
            {
                Console.WriteLine("Message not sent");
            }
        }
        public string[] Recieve()
        {
            string[] recievedMessageContent;
            recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            recievedMessage = CleanMessage(recievedMessage);
            recievedMessageContent = SortMessage(recievedMessage);
            return recievedMessageContent;

        }
        private string[] SortMessage(byte[] recievedMessage)
        {
            string[] recievedMessageContent = new string[3];
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
                Console.WriteLine("Abort was here");
                }
        }
    }
}
