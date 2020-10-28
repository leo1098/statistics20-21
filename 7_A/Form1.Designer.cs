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
            this.loadFileButton = new System.Windows.Forms.Button();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.filenameBox = new System.Windows.Forms.ListBox();
            this.columnsTreeView = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.changeTypeButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.hasHeaderCheck = new System.Windows.Forms.CheckBox();
            this.readCSVButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.columnsForMeanCombobox = new System.Windows.Forms.ComboBox();
            this.computeMeanButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(816, 31);
            this.loadFileButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(121, 45);
            this.loadFileButton.TabIndex = 1;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.readFileButton_Click);
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.Location = new System.Drawing.Point(534, 30);
            this.browseFolderButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(127, 45);
            this.browseFolderButton.TabIndex = 2;
            this.browseFolderButton.Text = "Select...";
            this.browseFolderButton.UseVisualStyleBackColor = true;
            this.browseFolderButton.Click += new System.EventHandler(this.browseFolderButton_Click);
            // 
            // filenameBox
            // 
            this.filenameBox.AllowDrop = true;
            this.filenameBox.FormattingEnabled = true;
            this.filenameBox.ItemHeight = 20;
            this.filenameBox.Location = new System.Drawing.Point(14, 32);
            this.filenameBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filenameBox.Name = "filenameBox";
            this.filenameBox.Size = new System.Drawing.Size(514, 44);
            this.filenameBox.TabIndex = 4;
            this.filenameBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.filenameBox_DragDrop);
            this.filenameBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.filenameBox_DragEnter);
            // 
            // columnsTreeView
            // 
            this.columnsTreeView.Location = new System.Drawing.Point(7, 78);
            this.columnsTreeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.columnsTreeView.Name = "columnsTreeView";
            this.columnsTreeView.Size = new System.Drawing.Size(260, 292);
            this.columnsTreeView.TabIndex = 6;
            this.columnsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.columnsTreeView_AfterSelect);
            this.columnsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.columnsTreeView_NodeMouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Select a node to change its type";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Boolean",
            "Integer",
            "Double",
            "DateTime",
            "String"});
            this.comboBox1.Location = new System.Drawing.Point(7, 410);
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
            this.label3.Size = new System.Drawing.Size(233, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Double click to change its name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.changeTypeButton);
            this.groupBox1.Controls.Add(this.columnsTreeView);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(22, 120);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(566, 502);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Column";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 386);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Type";
            // 
            // changeTypeButton
            // 
            this.changeTypeButton.Location = new System.Drawing.Point(7, 446);
            this.changeTypeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.changeTypeButton.Name = "changeTypeButton";
            this.changeTypeButton.Size = new System.Drawing.Size(261, 43);
            this.changeTypeButton.TabIndex = 11;
            this.changeTypeButton.Text = "Change Type";
            this.changeTypeButton.UseVisualStyleBackColor = true;
            this.changeTypeButton.Click += new System.EventHandler(this.changeTypeButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1032, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 12;
            this.textBox1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.hasHeaderCheck);
            this.groupBox2.Controls.Add(this.browseFolderButton);
            this.groupBox2.Controls.Add(this.filenameBox);
            this.groupBox2.Controls.Add(this.loadFileButton);
            this.groupBox2.Location = new System.Drawing.Point(22, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(943, 92);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drag&Drop a file here or browse the folders";
            // 
            // hasHeaderCheck
            // 
            this.hasHeaderCheck.AutoSize = true;
            this.hasHeaderCheck.Location = new System.Drawing.Point(667, 42);
            this.hasHeaderCheck.Name = "hasHeaderCheck";
            this.hasHeaderCheck.Size = new System.Drawing.Size(144, 24);
            this.hasHeaderCheck.TabIndex = 5;
            this.hasHeaderCheck.Text = "File has header";
            this.hasHeaderCheck.UseVisualStyleBackColor = true;
            // 
            // readCSVButton
            // 
            this.readCSVButton.Location = new System.Drawing.Point(651, 122);
            this.readCSVButton.Name = "readCSVButton";
            this.readCSVButton.Size = new System.Drawing.Size(115, 53);
            this.readCSVButton.TabIndex = 14;
            this.readCSVButton.Text = "Read CSV";
            this.readCSVButton.UseVisualStyleBackColor = true;
            this.readCSVButton.Click += new System.EventHandler(this.readCSVButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(616, 181);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.Size = new System.Drawing.Size(653, 410);
            this.dataGridView1.TabIndex = 15;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(349, 150);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(201, 441);
            this.richTextBox1.TabIndex = 16;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "A full line (for preview)";
            // 
            // columnsForMeanCombobox
            // 
            this.columnsForMeanCombobox.Enabled = false;
            this.columnsForMeanCombobox.FormattingEnabled = true;
            this.columnsForMeanCombobox.Location = new System.Drawing.Point(19, 25);
            this.columnsForMeanCombobox.Name = "columnsForMeanCombobox";
            this.columnsForMeanCombobox.Size = new System.Drawing.Size(201, 28);
            this.columnsForMeanCombobox.TabIndex = 18;
            // 
            // computeMeanButton
            // 
            this.computeMeanButton.Location = new System.Drawing.Point(226, 25);
            this.computeMeanButton.Name = "computeMeanButton";
            this.computeMeanButton.Size = new System.Drawing.Size(201, 37);
            this.computeMeanButton.TabIndex = 19;
            this.computeMeanButton.Text = "Compute Mean";
            this.computeMeanButton.UseVisualStyleBackColor = true;
            this.computeMeanButton.Click += new System.EventHandler(this.computeMeanButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextBox2);
            this.groupBox3.Controls.Add(this.computeMeanButton);
            this.groupBox3.Controls.Add(this.columnsForMeanCombobox);
            this.groupBox3.Location = new System.Drawing.Point(14, 626);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(648, 84);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select a field to compute mean";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(433, 26);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(172, 36);
            this.richTextBox2.TabIndex = 20;
            this.richTextBox2.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1472, 719);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.readCSVButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Button browseFolderButton;
        private System.Windows.Forms.ListBox filenameBox;
        private System.Windows.Forms.TreeView columnsTreeView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button changeTypeButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button readCSVButton;
        private System.Windows.Forms.CheckBox hasHeaderCheck;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox columnsForMeanCombobox;
        private System.Windows.Forms.Button computeMeanButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}

