﻿
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
            this.numericNBern = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label0 = new System.Windows.Forms.Label();
            this.numericMBern = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericP = new System.Windows.Forms.NumericUpDown();
            this.bernoulliPictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericEps = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericJBern = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.numericJRade = new System.Windows.Forms.NumericUpDown();
            this.rademacherPictureBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.numericNRade = new System.Windows.Forms.NumericUpDown();
            this.numericMRade = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericNBern)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMBern)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bernoulliPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJBern)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericJRade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rademacherPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNRade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMRade)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(623, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "Print Chart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.printSimulationButton_Click);
            // 
            // numericNBern
            // 
            this.numericNBern.Location = new System.Drawing.Point(66, 22);
            this.numericNBern.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericNBern.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericNBern.Name = "numericNBern";
            this.numericNBern.Size = new System.Drawing.Size(81, 22);
            this.numericNBern.TabIndex = 3;
            this.numericNBern.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "n";
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(243, 24);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(19, 17);
            this.label0.TabIndex = 6;
            this.label0.Text = "m";
            // 
            // numericMBern
            // 
            this.numericMBern.Location = new System.Drawing.Point(265, 24);
            this.numericMBern.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericMBern.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMBern.Name = "numericMBern";
            this.numericMBern.Size = new System.Drawing.Size(81, 22);
            this.numericMBern.TabIndex = 5;
            this.numericMBern.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 62);
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
            this.numericP.Location = new System.Drawing.Point(265, 62);
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
            // bernoulliPictureBox
            // 
            this.bernoulliPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bernoulliPictureBox.BackColor = System.Drawing.Color.Gainsboro;
            this.bernoulliPictureBox.Location = new System.Drawing.Point(23, 97);
            this.bernoulliPictureBox.Name = "bernoulliPictureBox";
            this.bernoulliPictureBox.Size = new System.Drawing.Size(1236, 539);
            this.bernoulliPictureBox.TabIndex = 9;
            this.bernoulliPictureBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(433, 43);
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
            this.numericEps.Location = new System.Drawing.Point(470, 43);
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
            this.label4.Location = new System.Drawing.Point(44, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "j";
            // 
            // numericJBern
            // 
            this.numericJBern.Location = new System.Drawing.Point(66, 59);
            this.numericJBern.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericJBern.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericJBern.Name = "numericJBern";
            this.numericJBern.Size = new System.Drawing.Size(81, 22);
            this.numericJBern.TabIndex = 12;
            this.numericJBern.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(25, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1292, 706);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bernoulliPictureBox);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.numericJBern);
            this.tabPage1.Controls.Add(this.numericNBern);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.numericEps);
            this.tabPage1.Controls.Add(this.numericMBern);
            this.tabPage1.Controls.Add(this.label0);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.numericP);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1284, 677);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bernoulli";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.numericJRade);
            this.tabPage2.Controls.Add(this.rademacherPictureBox);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.numericNRade);
            this.tabPage2.Controls.Add(this.numericMRade);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1284, 677);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Rademacher";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(93, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "j";
            // 
            // numericJRade
            // 
            this.numericJRade.Location = new System.Drawing.Point(115, 74);
            this.numericJRade.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericJRade.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericJRade.Name = "numericJRade";
            this.numericJRade.Size = new System.Drawing.Size(81, 22);
            this.numericJRade.TabIndex = 14;
            this.numericJRade.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // rademacherPictureBox
            // 
            this.rademacherPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rademacherPictureBox.BackColor = System.Drawing.Color.Gainsboro;
            this.rademacherPictureBox.Location = new System.Drawing.Point(53, 112);
            this.rademacherPictureBox.Name = "rademacherPictureBox";
            this.rademacherPictureBox.Size = new System.Drawing.Size(1198, 539);
            this.rademacherPictureBox.TabIndex = 12;
            this.rademacherPictureBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(276, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "m";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(651, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 43);
            this.button2.TabIndex = 6;
            this.button2.Text = "Print Chart";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericNRade
            // 
            this.numericNRade.Location = new System.Drawing.Point(115, 34);
            this.numericNRade.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericNRade.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericNRade.Name = "numericNRade";
            this.numericNRade.Size = new System.Drawing.Size(81, 22);
            this.numericNRade.TabIndex = 7;
            this.numericNRade.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // numericMRade
            // 
            this.numericMRade.Location = new System.Drawing.Point(314, 36);
            this.numericMRade.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericMRade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMRade.Name = "numericMRade";
            this.numericMRade.Size = new System.Drawing.Size(81, 22);
            this.numericMRade.TabIndex = 8;
            this.numericMRade.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 730);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericNBern)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMBern)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bernoulliPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJBern)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericJRade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rademacherPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNRade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMRade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericNBern;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.NumericUpDown numericMBern;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericP;
        private System.Windows.Forms.PictureBox bernoulliPictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericEps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericJBern;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox rademacherPictureBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numericNRade;
        private System.Windows.Forms.NumericUpDown numericMRade;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericJRade;
    }
}

