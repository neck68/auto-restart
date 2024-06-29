namespace AutoRestarter.Core.Forms
{
    partial class Main
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            AccountListView = new ListView();
            AddAccountButton = new Button();
            RemoveButton = new Button();
            WebhookCheckBox = new CheckBox();
            label3 = new Label();
            Hour = new NumericUpDown();
            Minute = new NumericUpDown();
            Second = new NumericUpDown();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            RefreshButton = new Button();
            ConnectButton = new Button();
            StartButton = new Button();
            StopButton = new Button();
            WebhookButton = new Button();
            ImageEditorButton = new Button();
            toolTip1 = new ToolTip(components);
            button1 = new Button();
            ConsoleCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)Hour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Minute).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Second).BeginInit();
            SuspendLayout();
            // 
            // AccountListView
            // 
            AccountListView.BackColor = SystemColors.ActiveBorder;
            AccountListView.Location = new Point(12, 13);
            AccountListView.Name = "AccountListView";
            AccountListView.Size = new Size(388, 181);
            AccountListView.TabIndex = 0;
            AccountListView.UseCompatibleStateImageBehavior = false;
            // 
            // AddAccountButton
            // 
            AddAccountButton.FlatStyle = FlatStyle.Flat;
            AddAccountButton.Font = new Font("Tahoma", 10F);
            AddAccountButton.Location = new Point(12, 205);
            AddAccountButton.Name = "AddAccountButton";
            AddAccountButton.Size = new Size(98, 30);
            AddAccountButton.TabIndex = 1;
            AddAccountButton.Text = "Add Account";
            toolTip1.SetToolTip(AddAccountButton, "Add accounts. (no login)");
            AddAccountButton.UseVisualStyleBackColor = true;
            AddAccountButton.Click += AddAccount_Click;
            // 
            // RemoveButton
            // 
            RemoveButton.FlatStyle = FlatStyle.Flat;
            RemoveButton.Font = new Font("Tahoma", 10F);
            RemoveButton.Location = new Point(116, 205);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(98, 31);
            RemoveButton.TabIndex = 2;
            RemoveButton.Text = "Remove";
            toolTip1.SetToolTip(RemoveButton, "Remove selected accounts.");
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += Remove_Click;
            // 
            // WebhookCheckBox
            // 
            WebhookCheckBox.AutoSize = true;
            WebhookCheckBox.ForeColor = SystemColors.ActiveCaptionText;
            WebhookCheckBox.Location = new Point(220, 211);
            WebhookCheckBox.Name = "WebhookCheckBox";
            WebhookCheckBox.Size = new Size(131, 21);
            WebhookCheckBox.TabIndex = 12;
            WebhookCheckBox.Text = "Enable Webhook";
            toolTip1.SetToolTip(WebhookCheckBox, "Should you get discord webhooks?");
            WebhookCheckBox.UseVisualStyleBackColor = true;
            WebhookCheckBox.CheckedChanged += WebhookBox_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ActiveBorder;
            label3.Location = new Point(597, -3);
            label3.Name = "label3";
            label3.Size = new Size(76, 17);
            label3.TabIndex = 8;
            label3.Text = "version 1.2";
            // 
            // Hour
            // 
            Hour.BackColor = SystemColors.ActiveBorder;
            Hour.BorderStyle = BorderStyle.None;
            Hour.Location = new Point(406, 33);
            Hour.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            Hour.Name = "Hour";
            Hour.Size = new Size(80, 20);
            Hour.TabIndex = 9;
            toolTip1.SetToolTip(Hour, "How long before it \"ticks\"");
            // 
            // Minute
            // 
            Minute.BackColor = SystemColors.ActiveBorder;
            Minute.BorderStyle = BorderStyle.None;
            Minute.Location = new Point(492, 33);
            Minute.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            Minute.Name = "Minute";
            Minute.Size = new Size(80, 20);
            Minute.TabIndex = 10;
            toolTip1.SetToolTip(Minute, "How long before it \"ticks\"");
            // 
            // Second
            // 
            Second.BackColor = SystemColors.ActiveBorder;
            Second.BorderStyle = BorderStyle.None;
            Second.Location = new Point(578, 33);
            Second.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            Second.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            Second.Name = "Second";
            Second.Size = new Size(80, 20);
            Second.TabIndex = 11;
            toolTip1.SetToolTip(Second, "How long before it \"ticks\"");
            Second.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(406, 13);
            label4.Name = "label4";
            label4.Size = new Size(49, 17);
            label4.TabIndex = 12;
            label4.Text = "Hours:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(492, 13);
            label5.Name = "label5";
            label5.Size = new Size(59, 17);
            label5.TabIndex = 13;
            label5.Text = "Minutes:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(578, 13);
            label6.Name = "label6";
            label6.Size = new Size(65, 17);
            label6.TabIndex = 14;
            label6.Text = "Seconds:";
            // 
            // RefreshButton
            // 
            RefreshButton.FlatStyle = FlatStyle.Flat;
            RefreshButton.Location = new Point(406, 89);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(124, 31);
            RefreshButton.TabIndex = 3;
            RefreshButton.Text = "Refresh Devices";
            toolTip1.SetToolTip(RefreshButton, "Must be pressed after every change.");
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // ConnectButton
            // 
            ConnectButton.Enabled = false;
            ConnectButton.FlatStyle = FlatStyle.Flat;
            ConnectButton.Location = new Point(406, 126);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(124, 31);
            ConnectButton.TabIndex = 4;
            ConnectButton.Text = "Connect";
            toolTip1.SetToolTip(ConnectButton, "Connects to emulator(s).");
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // StartButton
            // 
            StartButton.Enabled = false;
            StartButton.FlatStyle = FlatStyle.Flat;
            StartButton.Location = new Point(406, 163);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(124, 30);
            StartButton.TabIndex = 5;
            StartButton.Text = "Start";
            toolTip1.SetToolTip(StartButton, "Starts the whole system.");
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // StopButton
            // 
            StopButton.Enabled = false;
            StopButton.FlatStyle = FlatStyle.Flat;
            StopButton.Font = new Font("Tahoma", 10F);
            StopButton.Location = new Point(536, 163);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(124, 30);
            StopButton.TabIndex = 8;
            StopButton.Text = "Stop";
            StopButton.TextAlign = ContentAlignment.TopCenter;
            toolTip1.SetToolTip(StopButton, "Stops the whole system.");
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // WebhookButton
            // 
            WebhookButton.FlatStyle = FlatStyle.Flat;
            WebhookButton.Location = new Point(536, 126);
            WebhookButton.Name = "WebhookButton";
            WebhookButton.Size = new Size(124, 31);
            WebhookButton.TabIndex = 7;
            WebhookButton.Text = "Edit Webhooks";
            toolTip1.SetToolTip(WebhookButton, "For those people who want to customize webhooks. ☺");
            WebhookButton.UseVisualStyleBackColor = true;
            WebhookButton.Click += WebhookButton_Click;
            // 
            // ImageEditorButton
            // 
            ImageEditorButton.FlatStyle = FlatStyle.Flat;
            ImageEditorButton.Location = new Point(536, 89);
            ImageEditorButton.Name = "ImageEditorButton";
            ImageEditorButton.Size = new Size(124, 31);
            ImageEditorButton.TabIndex = 6;
            ImageEditorButton.Text = "Edit Images";
            toolTip1.SetToolTip(ImageEditorButton, "Customize your own screenshots!");
            ImageEditorButton.UseVisualStyleBackColor = true;
            ImageEditorButton.Click += ImageButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(492, 209);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 13;
            button1.Text = "false fire";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // ConsoleCheckBox
            // 
            ConsoleCheckBox.AutoSize = true;
            ConsoleCheckBox.Checked = true;
            ConsoleCheckBox.CheckState = CheckState.Checked;
            ConsoleCheckBox.Location = new Point(585, 211);
            ConsoleCheckBox.Name = "ConsoleCheckBox";
            ConsoleCheckBox.Size = new Size(75, 21);
            ConsoleCheckBox.TabIndex = 15;
            ConsoleCheckBox.Text = "Console";
            ConsoleCheckBox.UseVisualStyleBackColor = true;
            ConsoleCheckBox.CheckedChanged += ConsoleCheckBox_CheckedChanged;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(120, 120, 120);
            ClientSize = new Size(668, 246);
            Controls.Add(ConsoleCheckBox);
            Controls.Add(ImageEditorButton);
            Controls.Add(button1);
            Controls.Add(WebhookButton);
            Controls.Add(StopButton);
            Controls.Add(StartButton);
            Controls.Add(ConnectButton);
            Controls.Add(RefreshButton);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(Second);
            Controls.Add(Minute);
            Controls.Add(Hour);
            Controls.Add(label3);
            Controls.Add(WebhookCheckBox);
            Controls.Add(RemoveButton);
            Controls.Add(AddAccountButton);
            Controls.Add(AccountListView);
            Font = new Font("Tahoma", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Roblox Auto-Restarter";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)Hour).EndInit();
            ((System.ComponentModel.ISupportInitialize)Minute).EndInit();
            ((System.ComponentModel.ISupportInitialize)Second).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView AccountListView;
        private Button AddAccountButton;
        private Button RemoveButton;
        private CheckBox WebhookCheckBox;
        private Label label3;
        private NumericUpDown Hour;
        private NumericUpDown Minute;
        private NumericUpDown Second;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button RefreshButton;
        private Button ConnectButton;
        private Button StartButton;
        private Button StopButton;
        private Button WebhookButton;
        private Button ImageEditorButton;
        private ToolTip toolTip1;
        private Button button1;
        private CheckBox ConsoleCheckBox;
    }
}