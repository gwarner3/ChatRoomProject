﻿namespace Client
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
            this.components = new System.ComponentModel.Container();
            this.activeUsersDisplay = new System.Windows.Forms.ListBox();
            this.Input = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.DisplayBox = new System.Windows.Forms.TextBox();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // activeUsersDisplay
            // 
            this.activeUsersDisplay.FormattingEnabled = true;
            this.activeUsersDisplay.ItemHeight = 16;
            this.activeUsersDisplay.Location = new System.Drawing.Point(12, 12);
            this.activeUsersDisplay.Name = "activeUsersDisplay";
            this.activeUsersDisplay.Size = new System.Drawing.Size(165, 420);
            this.activeUsersDisplay.TabIndex = 0;
            this.activeUsersDisplay.Click += new System.EventHandler(this.activeUsersDisplay_Click);
            // 
            // Input
            // 
            this.Input.Location = new System.Drawing.Point(196, 458);
            this.Input.Multiline = true;
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(618, 40);
            this.Input.TabIndex = 2;
            this.Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_KeyDown);
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
            // DisplayBox
            // 
            this.DisplayBox.Location = new System.Drawing.Point(196, 12);
            this.DisplayBox.Multiline = true;
            this.DisplayBox.Name = "DisplayBox";
            this.DisplayBox.ReadOnly = true;
            this.DisplayBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DisplayBox.Size = new System.Drawing.Size(618, 420);
            this.DisplayBox.TabIndex = 4;
            // 
            // Chatroom
            // 
            this.AcceptButton = this.Send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 510);
            this.Controls.Add(this.DisplayBox);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.activeUsersDisplay);
            this.Name = "Chatroom";
            this.Text = "Chatroom";
            this.Load += new System.EventHandler(this.Chatroom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.Button Send;
        public System.Windows.Forms.TextBox DisplayBox;
        private System.Windows.Forms.BindingSource usersBindingSource;
        public System.Windows.Forms.ListBox activeUsersDisplay;
    }
}