namespace LoonieBot.Win
{
    partial class FormTerminal
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
            groupBox1 = new GroupBox();
            buttonAccount = new Button();
            textBox1 = new TextBox();
            groupBox2 = new GroupBox();
            buttonSubscribe = new Button();
            textBox2 = new TextBox();
            buttonDisconnect = new Button();
            buttonConnect = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(buttonAccount);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(226, 18);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(763, 233);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Account";
            // 
            // buttonAccount
            // 
            buttonAccount.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonAccount.Location = new Point(671, 193);
            buttonAccount.Name = "buttonAccount";
            buttonAccount.Size = new Size(75, 23);
            buttonAccount.TabIndex = 1;
            buttonAccount.Text = "Account";
            buttonAccount.UseVisualStyleBackColor = true;
            buttonAccount.Click += buttonAccount_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(19, 33);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(727, 154);
            textBox1.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(buttonSubscribe);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Location = new Point(9, 374);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(980, 191);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Transactions";
            // 
            // buttonSubscribe
            // 
            buttonSubscribe.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSubscribe.Location = new Point(888, 156);
            buttonSubscribe.Name = "buttonSubscribe";
            buttonSubscribe.Size = new Size(75, 23);
            buttonSubscribe.TabIndex = 1;
            buttonSubscribe.Text = "Subscribe";
            buttonSubscribe.UseVisualStyleBackColor = true;
            buttonSubscribe.Click += buttonSubscribe_Click;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(20, 31);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(943, 119);
            textBox2.TabIndex = 0;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(26, 72);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(75, 23);
            buttonDisconnect.TabIndex = 2;
            buttonDisconnect.Text = "Disconnect";
            buttonDisconnect.UseVisualStyleBackColor = true;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(26, 34);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(75, 23);
            buttonConnect.TabIndex = 3;
            buttonConnect.Text = "Connect";
            buttonConnect.UseVisualStyleBackColor = true;
            // 
            // FormTerminal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1023, 595);
            Controls.Add(buttonConnect);
            Controls.Add(buttonDisconnect);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormTerminal";
            Text = "FormTerminal";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button buttonAccount;
        private TextBox textBox1;
        private GroupBox groupBox2;
        private TextBox textBox2;
        private Button buttonDisconnect;
        private Button buttonConnect;
        private Button buttonSubscribe;
    }
}