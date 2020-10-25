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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // readFileButton
            // 
            this.readFileButton.Location = new System.Drawing.Point(34, 28);
            this.readFileButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.readFileButton.Name = "readFileButton";
            this.readFileButton.Size = new System.Drawing.Size(106, 61);
            this.readFileButton.TabIndex = 1;
            this.readFileButton.Text = "Read File";
            this.readFileButton.UseVisualStyleBackColor = true;
            this.readFileButton.Click += new System.EventHandler(this.readFileButton_Click);
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.Location = new System.Drawing.Point(759, 44);
            this.browseFolderButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(127, 45);
            this.browseFolderButton.TabIndex = 2;
            this.browseFolderButton.Text = "Select...";
            this.browseFolderButton.UseVisualStyleBackColor = true;
            this.browseFolderButton.Click += new System.EventHandler(this.browseFolderButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Drag&Drop a file here or browse the folders";
            // 
            // filenameBox
            // 
            this.filenameBox.AllowDrop = true;
            this.filenameBox.FormattingEnabled = true;
            this.filenameBox.ItemHeight = 20;
            this.filenameBox.Location = new System.Drawing.Point(238, 44);
            this.filenameBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filenameBox.Name = "filenameBox";
            this.filenameBox.Size = new System.Drawing.Size(514, 44);
            this.filenameBox.TabIndex = 4;
            this.filenameBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.filenameBox_DragDrop);
            this.filenameBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.filenameBox_DragEnter);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(440, 141);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(312, 325);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // typesTreeView
            // 
            this.typesTreeView.Location = new System.Drawing.Point(7, 78);
            this.typesTreeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.typesTreeView.Name = "typesTreeView";
            this.typesTreeView.Size = new System.Drawing.Size(260, 292);
            this.typesTreeView.TabIndex = 6;
            this.typesTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.typesTreeView_NodeMouseClick);
            this.typesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.typesTreeView_NodeMouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Click on Field Name";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "System_Boolean",
            "System_Int32",
            "System_Int64",
            "System_Double",
            "System_DateTime",
            "System_String"});
            this.comboBox1.Location = new System.Drawing.Point(7, 432);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(260, 28);
            this.comboBox1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "to change its type or name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.changeTypeButton);
            this.groupBox1.Controls.Add(this.typesTreeView);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(34, 111);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(292, 516);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Column";
            // 
            // changeTypeButton
            // 
            this.changeTypeButton.Location = new System.Drawing.Point(7, 468);
            this.changeTypeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.changeTypeButton.Name = "changeTypeButton";
            this.changeTypeButton.Size = new System.Drawing.Size(261, 41);
            this.changeTypeButton.TabIndex = 11;
            this.changeTypeButton.Text = "Change Type";
            this.changeTypeButton.UseVisualStyleBackColor = true;
            this.changeTypeButton.Click += new System.EventHandler(this.changeTypeButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(440, 593);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(185, 26);
            this.textBox1.TabIndex = 12;
            this.textBox1.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Type";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 642);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.filenameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseFolderButton);
            this.Controls.Add(this.readFileButton);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}

