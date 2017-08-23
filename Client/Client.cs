using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        public Chatroom chatroom;
        public Client(string IP, int port)
        {
            chatroom = new Chatroom(this);
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(IP), port);
            stream = clientSocket.GetStream();
        }
        public void Send(string text)
        {
            string messageString = text;
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
        }
        public void Recieve()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            chatroom.DisplayBox.Text += Encoding.ASCII.GetString(recievedMessage);
        }
    }
}
