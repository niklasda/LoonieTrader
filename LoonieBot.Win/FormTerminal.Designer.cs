﻿namespace LoonieBot.Win
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
            textBoxAcc = new TextBox();
            groupBox2 = new GroupBox();
            buttonSubscribe = new Button();
            textBoxTrx = new TextBox();
            buttonDisconnect = new Button();
            buttonConnectDemo = new Button();
            buttonConnectLive = new Button();
            textBoxSymbol = new TextBox();
            textBoxPositions = new TextBox();
            buttonSymbol = new Button();
            buttonPos = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(buttonAccount);
            groupBox1.Controls.Add(textBoxAcc);
            groupBox1.Location = new Point(226, 18);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(953, 189);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Account";
            // 
            // buttonAccount
            // 
            buttonAccount.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonAccount.Location = new Point(861, 149);
            buttonAccount.Name = "buttonAccount";
            buttonAccount.Size = new Size(75, 23);
            buttonAccount.TabIndex = 1;
            buttonAccount.Text = "Account";
            buttonAccount.UseVisualStyleBackColor = true;
            buttonAccount.Click += buttonAccount_Click;
            // 
            // textBoxAcc
            // 
            textBoxAcc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxAcc.Location = new Point(19, 33);
            textBoxAcc.Multiline = true;
            textBoxAcc.Name = "textBoxAcc";
            textBoxAcc.ScrollBars = ScrollBars.Vertical;
            textBoxAcc.Size = new Size(917, 110);
            textBoxAcc.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(buttonSubscribe);
            groupBox2.Controls.Add(textBoxTrx);
            groupBox2.Location = new Point(9, 375);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1170, 261);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Transactions";
            // 
            // buttonSubscribe
            // 
            buttonSubscribe.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSubscribe.Location = new Point(1078, 226);
            buttonSubscribe.Name = "buttonSubscribe";
            buttonSubscribe.Size = new Size(75, 23);
            buttonSubscribe.TabIndex = 1;
            buttonSubscribe.Text = "&Subscribe";
            buttonSubscribe.UseVisualStyleBackColor = true;
            buttonSubscribe.Click += buttonSubscribe_Click;
            // 
            // textBoxTrx
            // 
            textBoxTrx.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTrx.Location = new Point(20, 31);
            textBoxTrx.Multiline = true;
            textBoxTrx.Name = "textBoxTrx";
            textBoxTrx.ScrollBars = ScrollBars.Vertical;
            textBoxTrx.Size = new Size(1133, 189);
            textBoxTrx.TabIndex = 0;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(26, 112);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(91, 27);
            buttonDisconnect.TabIndex = 2;
            buttonDisconnect.Text = "Disconnect";
            buttonDisconnect.UseVisualStyleBackColor = true;
            // 
            // buttonConnectDemo
            // 
            buttonConnectDemo.Location = new Point(26, 34);
            buttonConnectDemo.Name = "buttonConnectDemo";
            buttonConnectDemo.Size = new Size(114, 33);
            buttonConnectDemo.TabIndex = 3;
            buttonConnectDemo.Text = "Connect Demo";
            buttonConnectDemo.UseVisualStyleBackColor = true;
            buttonConnectDemo.Click += buttonConnectDemo_Click;
            // 
            // buttonConnectLive
            // 
            buttonConnectLive.Location = new Point(26, 73);
            buttonConnectLive.Name = "buttonConnectLive";
            buttonConnectLive.Size = new Size(114, 33);
            buttonConnectLive.TabIndex = 4;
            buttonConnectLive.Text = "Connect Live";
            buttonConnectLive.UseVisualStyleBackColor = true;
            buttonConnectLive.Click += buttonConnectLive_Click;
            // 
            // textBoxSymbol
            // 
            textBoxSymbol.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxSymbol.Location = new Point(28, 225);
            textBoxSymbol.Multiline = true;
            textBoxSymbol.Name = "textBoxSymbol";
            textBoxSymbol.ScrollBars = ScrollBars.Vertical;
            textBoxSymbol.Size = new Size(504, 126);
            textBoxSymbol.TabIndex = 5;
            // 
            // textBoxPositions
            // 
            textBoxPositions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            textBoxPositions.Location = new Point(700, 268);
            textBoxPositions.Multiline = true;
            textBoxPositions.Name = "textBoxPositions";
            textBoxPositions.Size = new Size(360, 101);
            textBoxPositions.TabIndex = 6;
            // 
            // buttonSymbol
            // 
            buttonSymbol.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSymbol.Location = new Point(553, 296);
            buttonSymbol.Name = "buttonSymbol";
            buttonSymbol.Size = new Size(75, 23);
            buttonSymbol.TabIndex = 7;
            buttonSymbol.Text = "ËURUSD";
            buttonSymbol.UseVisualStyleBackColor = true;
            buttonSymbol.Click += buttonSymbol_Click;
            // 
            // buttonPos
            // 
            buttonPos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonPos.Location = new Point(1087, 296);
            buttonPos.Name = "buttonPos";
            buttonPos.Size = new Size(75, 23);
            buttonPos.TabIndex = 7;
            buttonPos.Text = "Pos";
            buttonPos.UseVisualStyleBackColor = true;
            buttonPos.Click += buttonPos_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 665);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1191, 22);
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(86, 17);
            toolStripStatusLabel1.Text = "Not connected";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1191, 24);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.ShortcutKeyDisplayString = "";
            aboutToolStripMenuItem.Size = new Size(180, 22);
            aboutToolStripMenuItem.Text = "&About";
            // 
            // FormTerminal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1191, 687);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(buttonPos);
            Controls.Add(buttonSymbol);
            Controls.Add(textBoxPositions);
            Controls.Add(textBoxSymbol);
            Controls.Add(buttonConnectLive);
            Controls.Add(buttonConnectDemo);
            Controls.Add(buttonDisconnect);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MainMenuStrip = menuStrip1;
            Name = "FormTerminal";
            Text = "LoonieTerminal";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Button buttonAccount;
        private TextBox textBoxAcc;
        private GroupBox groupBox2;
        private TextBox textBoxTrx;
        private Button buttonDisconnect;
        private Button buttonConnectDemo;
        private Button buttonSubscribe;
        private Button buttonConnectLive;
        private TextBox textBoxSymbol;
        private TextBox textBoxPositions;
        private Button buttonSymbol;
        private Button buttonPos;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}