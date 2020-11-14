
namespace _13_A
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
            this.button1 = new System.Windows.Forms.Button();
            this.numericN = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label0 = new System.Windows.Forms.Label();
            this.numericM = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericP = new System.Windows.Forms.NumericUpDown();
            this.chartPictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericEps = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericJ = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJ)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "Print Chart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.printSimulationButton_Click);
            // 
            // numericN
            // 
            this.numericN.Location = new System.Drawing.Point(45, 121);
            this.numericN.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericN.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericN.Name = "numericN";
            this.numericN.Size = new System.Drawing.Size(81, 22);
            this.numericN.TabIndex = 3;
            this.numericN.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "n";
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(23, 234);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(19, 17);
            this.label0.TabIndex = 6;
            this.label0.Text = "m";
            // 
            // numericM
            // 
            this.numericM.Location = new System.Drawing.Point(45, 234);
            this.numericM.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericM.Name = "numericM";
            this.numericM.Size = new System.Drawing.Size(81, 22);
            this.numericM.TabIndex = 5;
            this.numericM.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 272);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "p";
            // 
            // numericP
            // 
            this.numericP.DecimalPlaces = 2;
            this.numericP.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericP.Location = new System.Drawing.Point(45, 272);
            this.numericP.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericP.Name = "numericP";
            this.numericP.Size = new System.Drawing.Size(81, 22);
            this.numericP.TabIndex = 7;
            this.numericP.Value = new decimal(new int[] {
            52,
            0,
            0,
            131072});
            // 
            // chartPictureBox
            // 
            this.chartPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartPictureBox.BackColor = System.Drawing.Color.Gainsboro;
            this.chartPictureBox.Location = new System.Drawing.Point(231, 50);
            this.chartPictureBox.Name = "chartPictureBox";
            this.chartPictureBox.Size = new System.Drawing.Size(947, 533);
            this.chartPictureBox.TabIndex = 9;
            this.chartPictureBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "eps";
            // 
            // numericEps
            // 
            this.numericEps.DecimalPlaces = 2;
            this.numericEps.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericEps.Location = new System.Drawing.Point(45, 371);
            this.numericEps.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericEps.Name = "numericEps";
            this.numericEps.Size = new System.Drawing.Size(81, 22);
            this.numericEps.TabIndex = 10;
            this.numericEps.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "j";
            // 
            // numericJ
            // 
            this.numericJ.Location = new System.Drawing.Point(45, 158);
            this.numericJ.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericJ.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericJ.Name = "numericJ";
            this.numericJ.Size = new System.Drawing.Size(81, 22);
            this.numericJ.TabIndex = 12;
            this.numericJ.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 604);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericJ);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericEps);
            this.Controls.Add(this.chartPictureBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericP);
            this.Controls.Add(this.label0);
            this.Controls.Add(this.numericM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericN);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.NumericUpDown numericM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericP;
        private System.Windows.Forms.PictureBox chartPictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericEps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericJ;
    }
}

