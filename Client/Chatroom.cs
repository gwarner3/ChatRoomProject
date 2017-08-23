using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Chatroom : Form
    {
        Client client; 
        public Chatroom(Client client)
        {
            this.client = client;
            InitializeComponent();
        }

        private void SendMessage(object sender, EventArgs e)
        {
            client.Send(Input.Text);
        }
    }
}
