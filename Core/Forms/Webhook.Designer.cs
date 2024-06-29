namespace AutoRestarter.Core.Forms
{
    partial class Webhook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Webhook));
            webhookJsonBox = new RichTextBox();
            saveButton = new Button();
            webhookBox = new ComboBox();
            SuspendLayout();
            // 
            // webhookJsonBox
            // 
            webhookJsonBox.BackColor = SystemColors.ActiveBorder;
            webhookJsonBox.BorderStyle = BorderStyle.None;
            webhookJsonBox.ForeColor = SystemColors.Window;
            webhookJsonBox.Location = new Point(12, 47);
            webhookJsonBox.Name = "webhookJsonBox";
            webhookJsonBox.Size = new Size(414, 310);
            webhookJsonBox.TabIndex = 1;
            webhookJsonBox.Text = "";
            // 
            // saveButton
            // 
            saveButton.FlatStyle = FlatStyle.Popup;
            saveButton.Location = new Point(12, 364);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(414, 25);
            saveButton.TabIndex = 2;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // webhookBox
            // 
            webhookBox.BackColor = SystemColors.ActiveBorder;
            webhookBox.FlatStyle = FlatStyle.Flat;
            webhookBox.FormattingEnabled = true;
            webhookBox.Items.AddRange(new object[] { "Disconnect Webhook", "Rejoin Webhook", "Emulator Webhook" });
            webhookBox.Location = new Point(12, 13);
            webhookBox.Name = "webhookBox";
            webhookBox.Size = new Size(414, 24);
            webhookBox.TabIndex = 0;
            webhookBox.SelectedIndexChanged += WebhookBox_SelectedIndexChanged;
            // 
            // Webhook
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(120, 120, 120);
            ClientSize = new Size(438, 401);
            Controls.Add(webhookBox);
            Controls.Add(saveButton);
            Controls.Add(webhookJsonBox);
            Font = new Font("Tahoma", 10F);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Webhook";
            Text = "Modify Webhook";
            Load += Webhook_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox webhookJsonBox;
        private Button saveButton;
        private ComboBox webhookBox;
    }
}