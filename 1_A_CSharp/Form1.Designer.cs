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
            this.clearTextButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.putTextButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // clearTextButton
            // 
            this.clearTextButton.Location = new System.Drawing.Point(51, 131);
            this.clearTextButton.Name = "clearTextButton";
            this.clearTextButton.Size = new System.Drawing.Size(114, 41);
            this.clearTextButton.TabIndex = 0;
            this.clearTextButton.Text = "Clear Text";
            this.clearTextButton.UseVisualStyleBackColor = true;
            this.clearTextButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(277, 40);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(365, 132);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "";
            this.textBox.MouseEnter += new System.EventHandler(this.textBox_MouseEnter);
            this.textBox.MouseLeave += new System.EventHandler(this.textBox_MouseLeave);
            // 
            // putTextButton
            // 
            this.putTextButton.Location = new System.Drawing.Point(51, 40);
            this.putTextButton.Name = "putTextButton";
            this.putTextButton.Size = new System.Drawing.Size(114, 43);
            this.putTextButton.TabIndex = 2;
            this.putTextButton.Text = "Put Text";
            this.putTextButton.UseVisualStyleBackColor = true;
            this.putTextButton.Click += new System.EventHandler(this.putTextButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.richTextBox1.Location = new System.Drawing.Point(277, 198);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(365, 109);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "Press on the \'Put Text\' and \'Clear Text\' buttons to\n add or remove some sample te" +
    "xt.\nMove the mouse in and out of the Textbox to\n change the background color eve" +
    "ry time :).\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 364);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.putTextButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.clearTextButton);
            this.Name = "Form1";
            this.Text = "1_A C#";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clearTextButton;
        private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.Button putTextButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

