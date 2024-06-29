namespace AutoRestarter.Core.Forms
{
    partial class ImageEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEditor));
            ImageListView = new ListView();
            RemoveButton = new Button();
            AddImageButton = new Button();
            ClearButton = new Button();
            InfoButton = new Button();
            SuspendLayout();
            // 
            // ImageListView
            // 
            ImageListView.BackColor = SystemColors.ActiveBorder;
            ImageListView.Location = new Point(12, 13);
            ImageListView.Name = "ImageListView";
            ImageListView.Size = new Size(604, 415);
            ImageListView.TabIndex = 0;
            ImageListView.UseCompatibleStateImageBehavior = false;
            ImageListView.SelectedIndexChanged += ImageListView_SelectedIndexChanged;
            // 
            // RemoveButton
            // 
            RemoveButton.FlatStyle = FlatStyle.Flat;
            RemoveButton.Font = new Font("Tahoma", 10F);
            RemoveButton.Location = new Point(116, 434);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(98, 33);
            RemoveButton.TabIndex = 3;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // AddImageButton
            // 
            AddImageButton.FlatStyle = FlatStyle.Flat;
            AddImageButton.Font = new Font("Tahoma", 10F);
            AddImageButton.Location = new Point(12, 434);
            AddImageButton.Name = "AddImageButton";
            AddImageButton.Size = new Size(98, 33);
            AddImageButton.TabIndex = 4;
            AddImageButton.Text = "Add Image";
            AddImageButton.UseVisualStyleBackColor = true;
            AddImageButton.Click += AddImage_Click;
            // 
            // ClearButton
            // 
            ClearButton.FlatStyle = FlatStyle.Flat;
            ClearButton.Font = new Font("Tahoma", 10F);
            ClearButton.Location = new Point(518, 434);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(98, 33);
            ClearButton.TabIndex = 5;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            // 
            // InfoButton
            // 
            InfoButton.FlatStyle = FlatStyle.Flat;
            InfoButton.Font = new Font("Tahoma", 10F);
            InfoButton.Location = new Point(414, 434);
            InfoButton.Name = "InfoButton";
            InfoButton.Size = new Size(98, 33);
            InfoButton.TabIndex = 6;
            InfoButton.Text = "Info";
            InfoButton.UseVisualStyleBackColor = true;
            InfoButton.Click += InfoButton_Click;
            // 
            // ImageEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(120, 120, 120);
            ClientSize = new Size(628, 480);
            Controls.Add(InfoButton);
            Controls.Add(ClearButton);
            Controls.Add(AddImageButton);
            Controls.Add(RemoveButton);
            Controls.Add(ImageListView);
            Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ImageEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Image Editor";
            Load += Image_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView ImageListView;
        private Button RemoveButton;
        private Button AddImageButton;
        private Button ClearButton;
        private Button InfoButton;
    }
}