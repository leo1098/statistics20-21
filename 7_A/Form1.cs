﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _7_A
{
    public partial class Form1 : Form
    {
        enum dataType
        {
            System_EmptyString = -1,
            System_Boolean = 0,
            System_Int32 = 1,
            System_Int64 = 2,
            System_Double = 3,
            System_DateTime = 4,
            System_String = 5
        }

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

                    bool NamesSuggested = false;

                    // Types Detected by the program
                    List<TypeInfo> ListOfTypes = new List<TypeInfo>();

                    // first line could contains the column names
                    string[] FirstRow = reader.ReadLine().Split(',');
                    // first row without null values
                    string[] FirstFullRow = firstFullRow(FilePath);

                    // if first row is all strings and first full row is NOT all strings,
                    // the first line could be the header of the CSV file. I add those strings
                    // as column names
                    if (rowIsAllStrings(FirstRow) && !rowIsAllStrings(FirstFullRow))
                    {
                        NamesSuggested = true;
                        foreach (string ColumnName in FirstRow)
                            ListOfTypes.Add(new TypeInfo() { Name = ColumnName });
                    }

                    // the first column was not the header one, i should suggest names based on types

                    // determine data types
                    determineDataTypes(FirstFullRow, ListOfTypes, NamesSuggested);


                    printTypesInTree(ListOfTypes);

                    // determine data types
                    // determineDataTypes(FirstFullRow, ListOfTypes);






                    // will contains unit of observation
                    // List<UnitOfObservation> Units = new List<UnitOfObservation>();

                    //while (!reader.EndOfData)
                    //{
                    //    string[] Values = reader.ReadFields();
                    //    richTextBox1.AppendText(Values.ToString() + nl);

                    //    //UnitOfObservation U = new UnitOfObservation(Values[0], Int32.Parse(Values[1]));
                    //    UnitOfObservation U = new UnitOfObservation();

                    //    Type UnitType = U.GetType();
                    //    FieldInfo[] UnitFields = UnitType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    //    int i = 0;

                    //    foreach (FieldInfo F in UnitFields)
                    //    {
                    //        if (!string.IsNullOrWhiteSpace(Values[i]))
                    //        {
                    //            Object V = Convert.ChangeType(Values[i], F.FieldType);
                    //            F.SetValue(U, V);
                    //            i += 1;
                    //        }
                    //    }

                    //    Units.Add(U);
                    //}
                }
            }
        }

        private void determineDataTypes(string[] firstFullRow, List<TypeInfo> listOfTypes, bool namesSuggested)
        {
            dataType t;
            int[] indexes = new int[6]; // holds index for each data type

            if (!namesSuggested)
                for (int i = 0; i < firstFullRow.Length; i++)
                    listOfTypes.Add(new TypeInfo() { });

            for (int i = 0; i < firstFullRow.Length; i++)
            {
                t = ParseString(firstFullRow[i]);
                listOfTypes[i].InferredType = t.ToString();
                // if the name hasn't been suggested, i out a sample name
                if (string.IsNullOrEmpty(listOfTypes[i].Name))
                {
                    listOfTypes[i].Name = t.ToString() +"_"+ indexes[(int)t];
                    indexes[(int)t]++;
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

        private bool rowIsAllStrings(string[] row)
        {
            foreach (string value in row)
                if (ParseString(value) != dataType.System_String) return false;

            return true;
        }

        private dataType ParseString(string str)
        {

            bool boolValue;
            Int32 intValue;
            Int64 bigintValue;
            double doubleValue;
            DateTime dateValue;

            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return dataType.System_EmptyString;
            else if (bool.TryParse(str, out boolValue))
                return dataType.System_Boolean;
            else if (Int32.TryParse(str, out intValue))
                return dataType.System_Int32;
            else if (Int64.TryParse(str, out bigintValue))
                return dataType.System_Int64;
            else if (double.TryParse(str, out doubleValue))
                return dataType.System_Double;
            else if (DateTime.TryParse(str, out dateValue))
                return dataType.System_DateTime;
            else return dataType.System_String;

        }

        private string[] firstFullRow(string FilePath)
        {
            using (TextFieldParser reader = new TextFieldParser(FilePath))
            {
                reader.SetDelimiters(new string[] { "," });
                reader.CommentTokens = new string[] { "#" };
                reader.HasFieldsEnclosedInQuotes = true;

                //first line
                string[] ColumnNames = reader.ReadLine().Split(',');

                while (!reader.EndOfData)
                {
                    string[] values = reader.ReadFields();
                    bool Empty = values.Any(v => (string.IsNullOrEmpty(v) || string.IsNullOrWhiteSpace(v)));

                    if (Empty)
                        continue;
                    else
                        return values;
                }
                throw new Exception("The File doesn't contain any row with all valid values");
            }
        }

        private void printTypesInTree(List<TypeInfo> ListOfTypes)
        {
            int i = 0;
            foreach (TypeInfo T in ListOfTypes)
            {
                typesTreeView.Nodes.Add(T.Name);
                typesTreeView.Nodes[i].Nodes.Add("Inferred: " + T.InferredType);
                typesTreeView.Nodes[i].Nodes.Add("Actual: " + T.ActualType);
                i++;
            }
        }

        private void typesTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.comboBox1.Enabled = true;

            MessageBox.Show("Select the preferred data type from the combo box");
        }

        private void changeTypeButton_Click(object sender, EventArgs e)
        {
            int index;
            // if a child node is clicked, we retrieve its parent index
            if (this.typesTreeView.SelectedNode.Index > 0)
                index = this.typesTreeView.SelectedNode.Parent.Index;
            else
                index = this.typesTreeView.SelectedNode.Index;

            string SelectedType = this.comboBox1.SelectedItem.ToString();
        }
    }
}
