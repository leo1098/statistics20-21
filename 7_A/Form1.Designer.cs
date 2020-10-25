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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.changeTypeButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            this.typesTreeView.Location = new System.Drawing.Point(6, 62);
            this.typesTreeView.Name = "typesTreeView";
            this.typesTreeView.Size = new System.Drawing.Size(232, 261);
            this.typesTreeView.TabIndex = 6;
            this.typesTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.typesTreeView_NodeMouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Click on Field Name";
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
            "DateTime",
            "String"});
            this.comboBox1.Location = new System.Drawing.Point(6, 346);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(232, 24);
            this.comboBox1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "to change its type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.changeTypeButton);
            this.groupBox1.Controls.Add(this.typesTreeView);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(30, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 413);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Column";
            // 
            // changeTypeButton
            // 
            this.changeTypeButton.Location = new System.Drawing.Point(6, 376);
            this.changeTypeButton.Name = "changeTypeButton";
            this.changeTypeButton.Size = new System.Drawing.Size(232, 31);
            this.changeTypeButton.TabIndex = 11;
            this.changeTypeButton.Text = "Change Type";
            this.changeTypeButton.UseVisualStyleBackColor = true;
            this.changeTypeButton.Click += new System.EventHandler(this.changeTypeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 514);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.filenameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseFolderButton);
            this.Controls.Add(this.readFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button changeTypeButton;
    }
}

