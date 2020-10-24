using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace _7_A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string nl = Environment.NewLine;

        private void filenameBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void filenameBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
            {
                filenameBox.Items.Clear();
                filenameBox.Items.Add(s[i]);
            }
        }

        private void readFileButton_Click(object sender, EventArgs e)
        {
            // if no files are selected, display a MsgBox
            if (this.filenameBox.Items.Count <= 0)
            {
                string message = "No file selected";
                string title = "Select a file";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    // Do nothing
                }
                else
                {
                    throw new Exception("Error!");
                }
            }
            // a filename was inserted
            else
            {
                // Read the contents of the file into a stream
                string FilePath = this.filenameBox.Items[0].ToString().Trim();

                using (TextFieldParser reader = new TextFieldParser(FilePath))
                {
                    reader.SetDelimiters(new string[] { "," });
                    reader.CommentTokens = new string[] { "#" };
                    reader.HasFieldsEnclosedInQuotes = true;

                    // first line contains the column names
                    string[] ColumnNames = reader.ReadLine().Split(',');

                    // will contains unit of observation
                    List<UnitOfObservation> Units = new List<UnitOfObservation>();
                    
                    do
                    {
                        string[] Values = reader.ReadFields();
                        richTextBox1.AppendText(Values.ToString() + nl);

                        // UnitOfObservation U = new UnitOfObservation(Fields[0], Int32.Parse(Fields[1]));
                        UnitOfObservation U = new UnitOfObservation();

                        Type UnitType = U.GetType();
                        FieldInfo[] UnitFields = UnitType.GetFields();
                        int i = 0;

                        foreach (FieldInfo F in UnitFields)
                        {
                            if (!string.IsNullOrWhiteSpace(Values[i]))
                            {
                                Object V = Convert.ChangeType(Values[i], F.FieldType);
                                F.SetValue(U, V);
                                i += 1;
                            }
                        }
                        Units.Add(U);

                    } while (!reader.EndOfData);


                }
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e) // open a dialog box to select a file
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\guest 2\\Desktop";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    this.filenameBox.Items.Clear();
                    this.filenameBox.Items.Add(filePath);
                }
            }
        }
    }
}
