using System.Threading;
using System.Windows.Forms;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(IPFinder.GetLocalIPAddress(), 9999);
            Application.Run(client.chatroom);
        }
    }
}
