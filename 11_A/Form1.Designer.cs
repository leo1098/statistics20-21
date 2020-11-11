namespace _11_A
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.riemannSumTextBox = new System.Windows.Forms.RichTextBox();
            this.stepsTextBox = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxRiemann = new System.Windows.Forms.PictureBox();
            this.pictureBoxLebesgue = new System.Windows.Forms.PictureBox();
            this.lebesgueSumTextBox = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRiemann)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLebesgue)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(60, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start Animation";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // riemannSumTextBox
            // 
            this.riemannSumTextBox.Location = new System.Drawing.Point(426, 76);
            this.riemannSumTextBox.Name = "riemannSumTextBox";
            this.riemannSumTextBox.Size = new System.Drawing.Size(93, 51);
            this.riemannSumTextBox.TabIndex = 1;
            this.riemannSumTextBox.Text = "";
            // 
            // stepsTextBox
            // 
            this.stepsTextBox.Location = new System.Drawing.Point(219, 37);
            this.stepsTextBox.Name = "stepsTextBox";
            this.stepsTextBox.Size = new System.Drawing.Size(46, 28);
            this.stepsTextBox.TabIndex = 4;
            this.stepsTextBox.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBoxRiemann
            // 
            this.pictureBoxRiemann.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxRiemann.Location = new System.Drawing.Point(60, 145);
            this.pictureBoxRiemann.Name = "pictureBoxRiemann";
            this.pictureBoxRiemann.Size = new System.Drawing.Size(500, 500);
            this.pictureBoxRiemann.TabIndex = 5;
            this.pictureBoxRiemann.TabStop = false;
            // 
            // pictureBoxLebesgue
            // 
            this.pictureBoxLebesgue.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxLebesgue.Location = new System.Drawing.Point(606, 145);
            this.pictureBoxLebesgue.Name = "pictureBoxLebesgue";
            this.pictureBoxLebesgue.Size = new System.Drawing.Size(500, 500);
            this.pictureBoxLebesgue.TabIndex = 6;
            this.pictureBoxLebesgue.TabStop = false;
            // 
            // lebesgueSumTextBox
            // 
            this.lebesgueSumTextBox.Location = new System.Drawing.Point(959, 76);
            this.lebesgueSumTextBox.Name = "lebesgueSumTextBox";
            this.lebesgueSumTextBox.Size = new System.Drawing.Size(93, 51);
            this.lebesgueSumTextBox.TabIndex = 7;
            this.lebesgueSumTextBox.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(60, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 52);
            this.button3.TabIndex = 9;
            this.button3.Text = "Pause / Play Animation";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "num of steps";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(426, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Riemann Integral";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(956, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Lebesgue Integral";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(618, 14);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(196, 92);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "The example computes the integral of the function\ny = 300 - x from 0 to 300 using" +
    " both the Riemann \nand Lebesgue Integral";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 773);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lebesgueSumTextBox);
            this.Controls.Add(this.pictureBoxLebesgue);
            this.Controls.Add(this.pictureBoxRiemann);
            this.Controls.Add(this.stepsTextBox);
            this.Controls.Add(this.riemannSumTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRiemann)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLebesgue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox riemannSumTextBox;
        private System.Windows.Forms.RichTextBox stepsTextBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBoxRiemann;
        private System.Windows.Forms.PictureBox pictureBoxLebesgue;
        private System.Windows.Forms.RichTextBox lebesgueSumTextBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

