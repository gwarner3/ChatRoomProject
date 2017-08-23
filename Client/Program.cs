using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";
            Client client = new Client(IPFinder.GetLocalIPAddress(), 9999);
            Application.Run(client.chatroom);

        }
    }
}
