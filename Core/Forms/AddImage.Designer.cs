namespace AutoRestarter.Core.Forms
{
    partial class AddImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddImage));
            XBox = new TextBox();
            SubmitButton = new Button();
            ImageSelectButton = new Button();
            label1 = new Label();
            label2 = new Label();
            YBox = new TextBox();
            label3 = new Label();
            WidthBox = new TextBox();
            HeightBox = new TextBox();
            label4 = new Label();
            PictureBox = new PictureBox();
            label5 = new Label();
            PlaceHolderLabel = new Label();
            IncludeCheckBox = new CheckBox();
            ExcludeCheckBox = new CheckBox();
            label6 = new Label();
            ThresholdTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox).BeginInit();
            SuspendLayout();
            // 
            // XBox
            // 
            XBox.BackColor = SystemColors.ActiveBorder;
            XBox.BorderStyle = BorderStyle.None;
            XBox.Location = new Point(12, 100);
            XBox.Name = "XBox";
            XBox.Size = new Size(213, 16);
            XBox.TabIndex = 1;
            // 
            // SubmitButton
            // 
            SubmitButton.FlatStyle = FlatStyle.Flat;
            SubmitButton.Location = new Point(12, 362);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new Size(213, 31);
            SubmitButton.TabIndex = 8;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += SubmitButton_Click_1;
            // 
            // ImageSelectButton
            // 
            ImageSelectButton.FlatStyle = FlatStyle.Flat;
            ImageSelectButton.Location = new Point(12, 12);
            ImageSelectButton.Name = "ImageSelectButton";
            ImageSelectButton.Size = new Size(213, 31);
            ImageSelectButton.TabIndex = 0;
            ImageSelectButton.Text = "Select Image";
            ImageSelectButton.UseVisualStyleBackColor = true;
            ImageSelectButton.Click += ImageSelectButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 81);
            label1.Name = "label1";
            label1.Size = new Size(20, 16);
            label1.TabIndex = 8;
            label1.Text = "X:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 119);
            label2.Name = "label2";
            label2.Size = new Size(19, 16);
            label2.TabIndex = 10;
            label2.Text = "Y:";
            // 
            // YBox
            // 
            YBox.BackColor = SystemColors.ActiveBorder;
            YBox.BorderStyle = BorderStyle.None;
            YBox.Location = new Point(12, 138);
            YBox.Name = "YBox";
            YBox.Size = new Size(213, 16);
            YBox.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 157);
            label3.Name = "label3";
            label3.Size = new Size(45, 16);
            label3.TabIndex = 11;
            label3.Text = "Width:";
            // 
            // WidthBox
            // 
            WidthBox.BackColor = SystemColors.ActiveBorder;
            WidthBox.BorderStyle = BorderStyle.None;
            WidthBox.Location = new Point(12, 176);
            WidthBox.Name = "WidthBox";
            WidthBox.Size = new Size(213, 16);
            WidthBox.TabIndex = 3;
            // 
            // HeightBox
            // 
            HeightBox.BackColor = SystemColors.ActiveBorder;
            HeightBox.BorderStyle = BorderStyle.None;
            HeightBox.Location = new Point(12, 214);
            HeightBox.Name = "HeightBox";
            HeightBox.Size = new Size(213, 16);
            HeightBox.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 195);
            label4.Name = "label4";
            label4.Size = new Size(48, 16);
            label4.TabIndex = 14;
            label4.Text = "Height:";
            // 
            // PictureBox
            // 
            PictureBox.Location = new Point(241, 12);
            PictureBox.Name = "PictureBox";
            PictureBox.Size = new Size(380, 380);
            PictureBox.TabIndex = 15;
            PictureBox.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 46);
            label5.Name = "label5";
            label5.Size = new Size(85, 16);
            label5.TabIndex = 16;
            label5.Text = "Image Name:";
            // 
            // PlaceHolderLabel
            // 
            PlaceHolderLabel.AutoSize = true;
            PlaceHolderLabel.Location = new Point(12, 62);
            PlaceHolderLabel.Name = "PlaceHolderLabel";
            PlaceHolderLabel.Size = new Size(74, 16);
            PlaceHolderLabel.TabIndex = 17;
            PlaceHolderLabel.Text = "placeHolder";
            // 
            // IncludeCheckBox
            // 
            IncludeCheckBox.AutoSize = true;
            IncludeCheckBox.Location = new Point(12, 277);
            IncludeCheckBox.Name = "IncludeCheckBox";
            IncludeCheckBox.Size = new Size(67, 20);
            IncludeCheckBox.TabIndex = 6;
            IncludeCheckBox.Text = "Include";
            IncludeCheckBox.UseVisualStyleBackColor = true;
            // 
            // ExcludeCheckBox
            // 
            ExcludeCheckBox.AutoSize = true;
            ExcludeCheckBox.Location = new Point(158, 277);
            ExcludeCheckBox.Name = "ExcludeCheckBox";
            ExcludeCheckBox.Size = new Size(69, 20);
            ExcludeCheckBox.TabIndex = 7;
            ExcludeCheckBox.Text = "Exclude";
            ExcludeCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 233);
            label6.Name = "label6";
            label6.Size = new Size(110, 16);
            label6.TabIndex = 19;
            label6.Text = "Threshold (0 - 1):";
            // 
            // ThresholdTextBox
            // 
            ThresholdTextBox.BackColor = SystemColors.ActiveBorder;
            ThresholdTextBox.BorderStyle = BorderStyle.None;
            ThresholdTextBox.Location = new Point(12, 255);
            ThresholdTextBox.Name = "ThresholdTextBox";
            ThresholdTextBox.Size = new Size(213, 16);
            ThresholdTextBox.TabIndex = 5;
            // 
            // AddImage
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(120, 120, 120);
            ClientSize = new Size(633, 405);
            Controls.Add(ThresholdTextBox);
            Controls.Add(label6);
            Controls.Add(ExcludeCheckBox);
            Controls.Add(IncludeCheckBox);
            Controls.Add(PlaceHolderLabel);
            Controls.Add(label5);
            Controls.Add(PictureBox);
            Controls.Add(label4);
            Controls.Add(HeightBox);
            Controls.Add(WidthBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(YBox);
            Controls.Add(label1);
            Controls.Add(ImageSelectButton);
            Controls.Add(SubmitButton);
            Controls.Add(XBox);
            Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddImage";
            Text = "Add Image";
            Load += AddImage_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox XBox;
        private Button SubmitButton;
        private Button ImageSelectButton;
        private Label label1;
        private Label label2;
        private TextBox YBox;
        private Label label3;
        private TextBox WidthBox;
        private TextBox HeightBox;
        private Label label4;
        private PictureBox PictureBox;
        private Label label5;
        private Label PlaceHolderLabel;
        private CheckBox IncludeCheckBox;
        private CheckBox ExcludeCheckBox;
        private Label label6;
        private TextBox ThresholdTextBox;
    }
}