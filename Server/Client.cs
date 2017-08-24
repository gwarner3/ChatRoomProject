using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Client
    {
        public byte[] recievedMessage;
        public NetworkStream stream;
        TcpClient client;
        public string UserId;
        public Client(NetworkStream Stream, TcpClient Client)
        {
            stream = Stream;
            client = Client;
        }
        public void Send(string Message)
        {
            byte[] message = Encoding.ASCII.GetBytes(Message);
            stream.Write(message, 0, message.Count());
        }
        public string Recieve()
        {
            recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            recievedMessage = CleanMessage(recievedMessage);
            string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
            return recievedMessageString;

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
    }
}
