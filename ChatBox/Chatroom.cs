using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBox
{
    public partial class Chatroom : Form
    {
        Client.Client client;
        public Chatroom()
        {
            client = new Client.Client("192.168.0.138", 9999);

            InitializeComponent();
        }

        private void Send(object sender, EventArgs e)
        {
            client.Send(Input.Text);
        }
    }
}
