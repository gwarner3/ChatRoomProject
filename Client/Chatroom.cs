using System;
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
            Input.Text = "";
        }
        public void DisplayMessages(string message)
        {
            DisplayBox.AppendText(message);
        }
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                Input.SelectAll();
            }
        }

        private void Chatroom_Load(object sender, EventArgs e)
        {
            DisplayBox.Text = $"Hello {client.Username}! Welcome to George's Chat house, You are connected.";
        }

        private void activeUsersDisplay_Click(object sender, EventArgs e)
        {
            Input.Text = $"/pm({activeUsersDisplay.SelectedItem.ToString()})";
        }
    }
}
