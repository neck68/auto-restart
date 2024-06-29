namespace AutoRestarter.Core.Forms
{
    partial class AddAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAccount));
            UsernameTextBox = new TextBox();
            label1 = new Label();
            AdbTextBox = new TextBox();
            label2 = new Label();
            SubmitButton = new Button();
            label3 = new Label();
            WebhookTextBox = new TextBox();
            label4 = new Label();
            PrivateServerTextBox = new TextBox();
            SuspendLayout();
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.BackColor = SystemColors.ActiveBorder;
            UsernameTextBox.BorderStyle = BorderStyle.None;
            UsernameTextBox.Location = new Point(12, 32);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(213, 16);
            UsernameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(102, 16);
            label1.TabIndex = 1;
            label1.Text = "Username/Alias:";
            // 
            // AdbTextBox
            // 
            AdbTextBox.BackColor = SystemColors.ActiveBorder;
            AdbTextBox.BorderStyle = BorderStyle.None;
            AdbTextBox.Location = new Point(12, 71);
            AdbTextBox.Name = "AdbTextBox";
            AdbTextBox.Size = new Size(213, 16);
            AdbTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 52);
            label2.Name = "label2";
            label2.Size = new Size(62, 16);
            label2.TabIndex = 3;
            label2.Text = "ADB Port:";
            // 
            // SubmitButton
            // 
            SubmitButton.FlatStyle = FlatStyle.Flat;
            SubmitButton.Location = new Point(12, 182);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new Size(213, 25);
            SubmitButton.TabIndex = 5;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += Submit_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 92);
            label3.Name = "label3";
            label3.Size = new Size(65, 16);
            label3.TabIndex = 6;
            label3.Text = "Webhook:";
            // 
            // WebhookTextBox
            // 
            WebhookTextBox.BackColor = SystemColors.ActiveBorder;
            WebhookTextBox.BorderStyle = BorderStyle.None;
            WebhookTextBox.Location = new Point(12, 111);
            WebhookTextBox.Name = "WebhookTextBox";
            WebhookTextBox.Size = new Size(213, 16);
            WebhookTextBox.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 131);
            label4.Name = "label4";
            label4.Size = new Size(93, 16);
            label4.TabIndex = 8;
            label4.Text = "Private Server:";
            // 
            // PrivateServerTextBox
            // 
            PrivateServerTextBox.BackColor = SystemColors.ActiveBorder;
            PrivateServerTextBox.BorderStyle = BorderStyle.None;
            PrivateServerTextBox.Location = new Point(12, 150);
            PrivateServerTextBox.Name = "PrivateServerTextBox";
            PrivateServerTextBox.Size = new Size(213, 16);
            PrivateServerTextBox.TabIndex = 4;
            // 
            // AddAccount
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(120, 120, 120);
            ClientSize = new Size(240, 220);
            Controls.Add(label4);
            Controls.Add(PrivateServerTextBox);
            Controls.Add(label3);
            Controls.Add(WebhookTextBox);
            Controls.Add(SubmitButton);
            Controls.Add(label2);
            Controls.Add(AdbTextBox);
            Controls.Add(label1);
            Controls.Add(UsernameTextBox);
            Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddAccount";
            Text = "Add Account";
            Load += AddAccount_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox UsernameTextBox;
        private Label label1;
        private TextBox AdbTextBox;
        private Label label2;
        private Button SubmitButton;
        private Label label3;
        private TextBox WebhookTextBox;
        private Label label4;
        private TextBox PrivateServerTextBox;
    }
}