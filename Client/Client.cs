using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        Thread Receiver;
        public Chatroom chatroom;
        public Client(string IP, int port)
        {
            chatroom = new Chatroom(this);
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse("192.168.0.138"), port);
            chatroom.DisplayBox.Text += "Welcome to George's Chat house, You are connected.";
            stream = clientSocket.GetStream();
            Receiver = new Thread(new ThreadStart(() => Recieve()));
            Receiver.Start();
        }
        public void Send(string text)
        {
            string messageString = text;
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
        }
        public void Recieve()
        {
            while (true)
            {
                try
                {
                    byte[] recievedMessage = new byte[256];
                    stream.Read(recievedMessage, 0, recievedMessage.Length);
                    string message = Encoding.ASCII.GetString(recievedMessage);
                    Console.WriteLine(message);
                    chatroom.DisplayBox.Text += message;
                }
                catch
                {
                    
                }
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
