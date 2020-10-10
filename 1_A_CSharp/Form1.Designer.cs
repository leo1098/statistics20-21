namespace _1_A_CSharp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.clearTextButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.putTextButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // clearTextButton
            // 
            this.clearTextButton.Location = new System.Drawing.Point(45, 105);
            this.clearTextButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearTextButton.Name = "clearTextButton";
            this.clearTextButton.Size = new System.Drawing.Size(101, 33);
            this.clearTextButton.TabIndex = 0;
            this.clearTextButton.Text = "Clear Text";
            this.clearTextButton.UseVisualStyleBackColor = true;
            this.clearTextButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(246, 32);
            this.textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(407, 106);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "";
            this.textBox.MouseEnter += new System.EventHandler(this.textBox_MouseEnter);
            this.textBox.MouseLeave += new System.EventHandler(this.textBox_MouseLeave);
            // 
            // putTextButton
            // 
            this.putTextButton.Location = new System.Drawing.Point(45, 32);
            this.putTextButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.putTextButton.Name = "putTextButton";
            this.putTextButton.Size = new System.Drawing.Size(101, 34);
            this.putTextButton.TabIndex = 2;
            this.putTextButton.Text = "Put Text";
            this.putTextButton.UseVisualStyleBackColor = true;
            this.putTextButton.Click += new System.EventHandler(this.putTextButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.richTextBox1.Location = new System.Drawing.Point(246, 158);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(407, 88);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "Drag and Drop a file here..."});
            this.listBox1.Location = new System.Drawing.Point(246, 260);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(407, 84);
            this.listBox1.TabIndex = 4;
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 395);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.putTextButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.clearTextButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "1_A C#";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clearTextButton;
        private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.Button putTextButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListBox listBox1;
    }
}

