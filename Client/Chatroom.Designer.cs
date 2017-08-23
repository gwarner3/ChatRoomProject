namespace Client
{
    partial class Chatroom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.DisplayBox = new System.Windows.Forms.RichTextBox();
            this.Input = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(165, 420);
            this.listBox1.TabIndex = 0;
            // 
            // DisplayBox
            // 
            this.DisplayBox.Location = new System.Drawing.Point(196, 12);
            this.DisplayBox.Name = "DisplayBox";
            this.DisplayBox.ReadOnly = true;
            this.DisplayBox.Size = new System.Drawing.Size(681, 421);
            this.DisplayBox.TabIndex = 1;
            this.DisplayBox.Text = "";
            // 
            // Input
            // 
            this.Input.Location = new System.Drawing.Point(196, 458);
            this.Input.Multiline = true;
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(618, 40);
            this.Input.TabIndex = 2;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(828, 475);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 23);
            this.Send.TabIndex = 3;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.SendMessage);
            // 
            // Chatroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 510);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.DisplayBox);
            this.Controls.Add(this.listBox1);
            this.Name = "Chatroom";
            this.Text = "Chatroom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.RichTextBox DisplayBox;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.Button Send;
    }
}