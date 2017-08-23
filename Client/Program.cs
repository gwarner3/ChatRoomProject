using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Client client = new Client("192.168.0.138", 9999);
            Application.Run(client.chatroom);
        }
    }
}
