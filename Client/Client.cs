﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Threading;



namespace Client
{
    public class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        Thread Receiver;
        Thread Displayer;
        Queue<byte[]> messageQueue;
        public Chatroom chatroom;
        public string Username;

        public Client(string IP, int port)
        {
            GetUserName(); 
            chatroom = new Chatroom(this);
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse("192.168.0.126"), port); //IPFinder.GetLocalIPAddress()            
            stream = clientSocket.GetStream();
            messageQueue = new Queue<byte[]>();
            Displayer = new Thread(new ThreadStart(DisplayMessages));
            Send(Username);
            Receiver = new Thread(new ThreadStart(Recieve));
            Receiver.Start();
            Displayer.Start();
        }
        private void GetUserName()
        {
            Console.WriteLine("Please enter a username.");
            Username = Console.ReadLine();
        }
        public void Send(string text)
        {
            string messageString = text;
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
        }
        private void DisplayMessages()
        {
            byte[] recievedMessage;
            while (true)
            {
                if (messageQueue.Count > 0)
                {
                    recievedMessage = messageQueue.Dequeue();
                    recievedMessage = CleanMessage(recievedMessage);
                    string message = Encoding.ASCII.GetString(recievedMessage);
                    CheckMessageEncoding(message);
                }
            }
        }
        public void Recieve()
        {
            while (true)
            {
                try
                {
                    byte[] recievedMessage = new byte[256];
                    stream.Read(recievedMessage, 0, recievedMessage.Length);
                    Console.WriteLine(recievedMessage);
                    messageQueue.Enqueue(recievedMessage);   
                }
                catch
                {
                    Console.WriteLine("DISCONNECTED");
                }
            }
        }
        private void CheckMessageEncoding(string message)
        {
            if (message.StartsWith("/<>"))
            {
                chatroom.activeUsersDisplay.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () { chatroom.activeUsersDisplay.Items.Clear(); });
                string[] names = message.Substring(3).Split(';');
                foreach (string name in names)
                {
                    chatroom.activeUsersDisplay.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () { chatroom.activeUsersDisplay.Items.Add(name); });
                }
            }
            else
            {
                chatroom.DisplayBox.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () { chatroom.DisplayMessages(Environment.NewLine + message); });
            }
        }
        private byte[] CleanMessage(byte[] message)
        {
            try
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
            catch
            {
                chatroom.DisplayBox.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () { chatroom.DisplayMessages(Environment.NewLine + "This server has been disconnected."); });
                Thread.CurrentThread.Abort();
            }
            return message;
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
