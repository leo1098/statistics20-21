
namespace _15_A
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
            this.cdfPictureBox = new System.Windows.Forms.PictureBox();
            this.histogramPictureBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cdfPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start Simulation";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cdfPictureBox
            // 
            this.cdfPictureBox.BackColor = System.Drawing.Color.LightGray;
            this.cdfPictureBox.Location = new System.Drawing.Point(218, 12);
            this.cdfPictureBox.Name = "cdfPictureBox";
            this.cdfPictureBox.Size = new System.Drawing.Size(768, 351);
            this.cdfPictureBox.TabIndex = 1;
            this.cdfPictureBox.TabStop = false;
            // 
            // histogramPictureBox
            // 
            this.histogramPictureBox.BackColor = System.Drawing.Color.LightGray;
            this.histogramPictureBox.Location = new System.Drawing.Point(218, 426);
            this.histogramPictureBox.Name = "histogramPictureBox";
            this.histogramPictureBox.Size = new System.Drawing.Size(768, 361);
            this.histogramPictureBox.TabIndex = 2;
            this.histogramPictureBox.TabStop = false;
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 110);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 50);
            this.button2.TabIndex = 3;
            this.button2.Text = "Stop Simulation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 878);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.histogramPictureBox);
            this.Controls.Add(this.cdfPictureBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.cdfPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox cdfPictureBox;
        private System.Windows.Forms.PictureBox histogramPictureBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button button2;
    }
}

