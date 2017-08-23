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
            chatroom.DisplayBox.Text += "Welcome to George's Chat house, You are connected.";
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
            try
            {
                byte[] recievedMessage = new byte[256];
                stream.Read(recievedMessage, 0, recievedMessage.Length);
                chatroom.DisplayBox.Text += Encoding.ASCII.GetString(recievedMessage);
            }
            catch
            {
                Recieve();
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
