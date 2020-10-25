namespace CSV_Reader
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.readButton = new System.Windows.Forms.Button();
            this.variableGroupBox = new System.Windows.Forms.GroupBox();
            this.acceptHeaderButton = new System.Windows.Forms.Button();
            this.generateHeaderButton = new System.Windows.Forms.Button();
            this.headerCheckBox = new System.Windows.Forms.CheckBox();
            this.firstLineTextBox = new System.Windows.Forms.TextBox();
            this.variableTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.changeButton = new System.Windows.Forms.Button();
            this.stringRadioButton = new System.Windows.Forms.RadioButton();
            this.doubleRadioButton = new System.Windows.Forms.RadioButton();
            this.integerRadioButton = new System.Windows.Forms.RadioButton();
            this.variableComboBox = new System.Windows.Forms.ComboBox();
            this.typePreviewRichTextBox = new System.Windows.Forms.RichTextBox();
            this.importButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.statusLabel = new System.Windows.Forms.Label();
            this.computationComboBox = new System.Windows.Forms.ComboBox();
            this.computationButton = new System.Windows.Forms.Button();
            this.computationRichTextBox = new System.Windows.Forms.RichTextBox();
            this.stepTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.variableGroupBox.SuspendLayout();
            this.variableTypeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(540, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open File";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.AllowDrop = true;
            this.pathTextBox.Location = new System.Drawing.Point(12, 12);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(522, 20);
            this.pathTextBox.TabIndex = 1;
            this.pathTextBox.TextChanged += new System.EventHandler(this.pathTextBox_TextChanged);
            this.pathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.pathTextBox_DragDrop);
            this.pathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pathTextBox_DragEnter);
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(621, 12);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(75, 23);
            this.readButton.TabIndex = 2;
            this.readButton.Text = "Read File";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // variableGroupBox
            // 
            this.variableGroupBox.Controls.Add(this.acceptHeaderButton);
            this.variableGroupBox.Controls.Add(this.generateHeaderButton);
            this.variableGroupBox.Controls.Add(this.headerCheckBox);
            this.variableGroupBox.Controls.Add(this.firstLineTextBox);
            this.variableGroupBox.Location = new System.Drawing.Point(13, 39);
            this.variableGroupBox.Name = "variableGroupBox";
            this.variableGroupBox.Size = new System.Drawing.Size(374, 119);
            this.variableGroupBox.TabIndex = 3;
            this.variableGroupBox.TabStop = false;
            this.variableGroupBox.Text = "Variable Names";
            // 
            // acceptHeaderButton
            // 
            this.acceptHeaderButton.Enabled = false;
            this.acceptHeaderButton.Location = new System.Drawing.Point(236, 90);
            this.acceptHeaderButton.Name = "acceptHeaderButton";
            this.acceptHeaderButton.Size = new System.Drawing.Size(132, 23);
            this.acceptHeaderButton.TabIndex = 3;
            this.acceptHeaderButton.Text = "Accept Header";
            this.acceptHeaderButton.UseVisualStyleBackColor = true;
            this.acceptHeaderButton.Click += new System.EventHandler(this.acceptHeaderButton_Click);
            // 
            // generateHeaderButton
            // 
            this.generateHeaderButton.Location = new System.Drawing.Point(6, 90);
            this.generateHeaderButton.Name = "generateHeaderButton";
            this.generateHeaderButton.Size = new System.Drawing.Size(132, 23);
            this.generateHeaderButton.TabIndex = 2;
            this.generateHeaderButton.Text = "Generate Header";
            this.generateHeaderButton.UseVisualStyleBackColor = true;
            this.generateHeaderButton.Click += new System.EventHandler(this.generateHeaderButton_Click);
            // 
            // headerCheckBox
            // 
            this.headerCheckBox.AutoSize = true;
            this.headerCheckBox.Location = new System.Drawing.Point(6, 47);
            this.headerCheckBox.Name = "headerCheckBox";
            this.headerCheckBox.Size = new System.Drawing.Size(131, 17);
            this.headerCheckBox.TabIndex = 1;
            this.headerCheckBox.Text = "CSV File has a header";
            this.headerCheckBox.UseVisualStyleBackColor = true;
            this.headerCheckBox.CheckedChanged += new System.EventHandler(this.headerCheckBox_CheckedChanged);
            // 
            // firstLineTextBox
            // 
            this.firstLineTextBox.Location = new System.Drawing.Point(6, 21);
            this.firstLineTextBox.Name = "firstLineTextBox";
            this.firstLineTextBox.ReadOnly = true;
            this.firstLineTextBox.Size = new System.Drawing.Size(362, 20);
            this.firstLineTextBox.TabIndex = 0;
            // 
            // variableTypeGroupBox
            // 
            this.variableTypeGroupBox.Controls.Add(this.changeButton);
            this.variableTypeGroupBox.Controls.Add(this.stringRadioButton);
            this.variableTypeGroupBox.Controls.Add(this.doubleRadioButton);
            this.variableTypeGroupBox.Controls.Add(this.integerRadioButton);
            this.variableTypeGroupBox.Controls.Add(this.variableComboBox);
            this.variableTypeGroupBox.Location = new System.Drawing.Point(12, 165);
            this.variableTypeGroupBox.Name = "variableTypeGroupBox";
            this.variableTypeGroupBox.Size = new System.Drawing.Size(375, 195);
            this.variableTypeGroupBox.TabIndex = 4;
            this.variableTypeGroupBox.TabStop = false;
            this.variableTypeGroupBox.Text = "Variable Types";
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(6, 166);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(131, 23);
            this.changeButton.TabIndex = 5;
            this.changeButton.Text = "Change Type";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // stringRadioButton
            // 
            this.stringRadioButton.AutoSize = true;
            this.stringRadioButton.Location = new System.Drawing.Point(7, 102);
            this.stringRadioButton.Name = "stringRadioButton";
            this.stringRadioButton.Size = new System.Drawing.Size(52, 17);
            this.stringRadioButton.TabIndex = 3;
            this.stringRadioButton.TabStop = true;
            this.stringRadioButton.Text = "String";
            this.stringRadioButton.UseVisualStyleBackColor = true;
            // 
            // doubleRadioButton
            // 
            this.doubleRadioButton.AutoSize = true;
            this.doubleRadioButton.Location = new System.Drawing.Point(7, 79);
            this.doubleRadioButton.Name = "doubleRadioButton";
            this.doubleRadioButton.Size = new System.Drawing.Size(59, 17);
            this.doubleRadioButton.TabIndex = 2;
            this.doubleRadioButton.TabStop = true;
            this.doubleRadioButton.Text = "Double";
            this.doubleRadioButton.UseVisualStyleBackColor = true;
            // 
            // integerRadioButton
            // 
            this.integerRadioButton.AutoSize = true;
            this.integerRadioButton.Location = new System.Drawing.Point(7, 56);
            this.integerRadioButton.Name = "integerRadioButton";
            this.integerRadioButton.Size = new System.Drawing.Size(58, 17);
            this.integerRadioButton.TabIndex = 1;
            this.integerRadioButton.TabStop = true;
            this.integerRadioButton.Text = "Integer";
            this.integerRadioButton.UseVisualStyleBackColor = true;
            // 
            // variableComboBox
            // 
            this.variableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.variableComboBox.FormattingEnabled = true;
            this.variableComboBox.Location = new System.Drawing.Point(7, 19);
            this.variableComboBox.Name = "variableComboBox";
            this.variableComboBox.Size = new System.Drawing.Size(170, 21);
            this.variableComboBox.TabIndex = 0;
            // 
            // typePreviewRichTextBox
            // 
            this.typePreviewRichTextBox.Location = new System.Drawing.Point(394, 41);
            this.typePreviewRichTextBox.Name = "typePreviewRichTextBox";
            this.typePreviewRichTextBox.ReadOnly = true;
            this.typePreviewRichTextBox.Size = new System.Drawing.Size(303, 319);
            this.typePreviewRichTextBox.TabIndex = 5;
            this.typePreviewRichTextBox.Text = "";
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(394, 366);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(303, 23);
            this.importButton.TabIndex = 6;
            this.importButton.Text = "Import File";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView
            // 
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(13, 393);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(684, 356);
            this.dataGridView.TabIndex = 7;
            this.dataGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValidated);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(703, 736);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(37, 13);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "Status";
            // 
            // computationComboBox
            // 
            this.computationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.computationComboBox.FormattingEnabled = true;
            this.computationComboBox.Location = new System.Drawing.Point(703, 13);
            this.computationComboBox.Name = "computationComboBox";
            this.computationComboBox.Size = new System.Drawing.Size(339, 21);
            this.computationComboBox.TabIndex = 8;
            // 
            // computationButton
            // 
            this.computationButton.Location = new System.Drawing.Point(912, 41);
            this.computationButton.Name = "computationButton";
            this.computationButton.Size = new System.Drawing.Size(130, 23);
            this.computationButton.TabIndex = 9;
            this.computationButton.Text = "Compute";
            this.computationButton.UseVisualStyleBackColor = true;
            this.computationButton.Click += new System.EventHandler(this.computationButton_Click);
            // 
            // computationRichTextBox
            // 
            this.computationRichTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.computationRichTextBox.Location = new System.Drawing.Point(703, 86);
            this.computationRichTextBox.Name = "computationRichTextBox";
            this.computationRichTextBox.ReadOnly = true;
            this.computationRichTextBox.Size = new System.Drawing.Size(339, 274);
            this.computationRichTextBox.TabIndex = 10;
            this.computationRichTextBox.Text = "";
            // 
            // stepTextBox
            // 
            this.stepTextBox.Location = new System.Drawing.Point(779, 41);
            this.stepTextBox.Name = "stepTextBox";
            this.stepTextBox.Size = new System.Drawing.Size(70, 20);
            this.stepTextBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(703, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Interval width";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 761);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stepTextBox);
            this.Controls.Add(this.computationRichTextBox);
            this.Controls.Add(this.computationButton);
            this.Controls.Add(this.computationComboBox);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.typePreviewRichTextBox);
            this.Controls.Add(this.variableTypeGroupBox);
            this.Controls.Add(this.variableGroupBox);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.openButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "CSV Reader";
            this.variableGroupBox.ResumeLayout(false);
            this.variableGroupBox.PerformLayout();
            this.variableTypeGroupBox.ResumeLayout(false);
            this.variableTypeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.GroupBox variableGroupBox;
        private System.Windows.Forms.CheckBox headerCheckBox;
        private System.Windows.Forms.TextBox firstLineTextBox;
        private System.Windows.Forms.Button generateHeaderButton;
        private System.Windows.Forms.GroupBox variableTypeGroupBox;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.RadioButton stringRadioButton;
        private System.Windows.Forms.RadioButton doubleRadioButton;
        private System.Windows.Forms.RadioButton integerRadioButton;
        private System.Windows.Forms.ComboBox variableComboBox;
        private System.Windows.Forms.RichTextBox typePreviewRichTextBox;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button acceptHeaderButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox computationComboBox;
        private System.Windows.Forms.Button computationButton;
        private System.Windows.Forms.RichTextBox computationRichTextBox;
        private System.Windows.Forms.TextBox stepTextBox;
        private System.Windows.Forms.Label label1;
    }
}

