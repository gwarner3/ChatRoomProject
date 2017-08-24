using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public class Client
    {
        TcpClient clientSocket;
        public NetworkStream stream;
        Thread Receiver;
        public Chatroom chatroom;
        public string Username;
        public Users users = new Users();

        public Client(string IP, int port)
        {
            Console.WriteLine("please enter a username");
            Username = Console.ReadLine();
            chatroom = new Chatroom(this);
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse("192.168.0.138"), port);
            chatroom.DisplayBox.Text = "Welcome to George's Chat house, You are connected.";
            stream = clientSocket.GetStream();
            Send(Username);
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
                    recievedMessage = CleanMessage(recievedMessage);
                    string message = Encoding.ASCII.GetString(recievedMessage);
                    
                    Console.WriteLine(message);
                    chatroom.DisplayBox.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () { chatroom.DisplayMessages(Environment.NewLine + message); });
                }
                catch
                {
                    
                }
            }
        }
        private byte[] CleanMessage(byte[] message)
        {
            int i = message.Length -1;
            while(message[i] == 0)
            {
                --i;
            }
            byte[] CleanMessage = new byte[i + 1];
            Array.Copy(message, CleanMessage, i + 1);
            return CleanMessage;
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
