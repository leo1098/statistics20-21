namespace _7_A
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
            this.readFileButton = new System.Windows.Forms.Button();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.filenameBox = new System.Windows.Forms.ListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.typesTreeView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // readFileButton
            // 
            this.readFileButton.Location = new System.Drawing.Point(30, 22);
            this.readFileButton.Name = "readFileButton";
            this.readFileButton.Size = new System.Drawing.Size(94, 49);
            this.readFileButton.TabIndex = 1;
            this.readFileButton.Text = "Read File";
            this.readFileButton.UseVisualStyleBackColor = true;
            this.readFileButton.Click += new System.EventHandler(this.readFileButton_Click);
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.Location = new System.Drawing.Point(675, 35);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(113, 36);
            this.browseFolderButton.TabIndex = 2;
            this.browseFolderButton.Text = "Select...";
            this.browseFolderButton.UseVisualStyleBackColor = true;
            this.browseFolderButton.Click += new System.EventHandler(this.browseFolderButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Drag&Drop a file here or browse the folders";
            // 
            // filenameBox
            // 
            this.filenameBox.AllowDrop = true;
            this.filenameBox.FormattingEnabled = true;
            this.filenameBox.ItemHeight = 16;
            this.filenameBox.Location = new System.Drawing.Point(212, 35);
            this.filenameBox.Name = "filenameBox";
            this.filenameBox.Size = new System.Drawing.Size(457, 36);
            this.filenameBox.TabIndex = 4;
            this.filenameBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.filenameBox_DragDrop);
            this.filenameBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.filenameBox_DragEnter);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(391, 113);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(278, 261);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // typesTreeView
            // 
            this.typesTreeView.Location = new System.Drawing.Point(15, 48);
            this.typesTreeView.Name = "typesTreeView";
            this.typesTreeView.Size = new System.Drawing.Size(182, 261);
            this.typesTreeView.TabIndex = 6;
            this.typesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.typesTreeView_NodeMouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.typesTreeView);
            this.panel1.Location = new System.Drawing.Point(30, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 396);
            this.panel1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Double Click on Field Name";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Boolean",
            "Int_32",
            "Int_64",
            "Double",
            "String"});
            this.comboBox1.Location = new System.Drawing.Point(18, 328);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(179, 24);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "to change its type";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 514);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.filenameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseFolderButton);
            this.Controls.Add(this.readFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button readFileButton;
        private System.Windows.Forms.Button browseFolderButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox filenameBox;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TreeView typesTreeView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
    }
}

