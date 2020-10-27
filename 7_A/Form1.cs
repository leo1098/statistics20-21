using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace _7_A
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        // Useful Stuff --------------------------------------------------------
        string nl = Environment.NewLine;
        // first line could contains the column names
        string[] FirstRow;
        // first row without null values
        string[] FirstFullRow;
        // List with types for each column
        List<ColumnInfo> ListOfColumns;
        // actual dataset
        List<Dictionary<string, dynamic>> DataSet;
        // will contain filepath of csv
        string FilePath;
        
        bool HasHeader = false;


        // ------------------ HANDLERS -------------------------------
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
                DialogResult result = MessageBox.Show("No file selected", "Select a file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK) { }
                else throw new Exception("Error!");
            }
            // a filename was inserted
            else
            {
                // Read the contents of the file into a stream
                FilePath = this.filenameBox.Items[0].ToString().Trim();

                using (TextFieldParser reader = new TextFieldParser(FilePath))
                {
                    reader.SetDelimiters(new string[] { "," });
                    reader.CommentTokens = new string[] { "#" };
                    reader.HasFieldsEnclosedInQuotes = true;

                    // compute First Full Row, List Of Columns and First Row
                    FirstFullRow = firstFullRow(FilePath); // we print it to give a preview of the dataset     

                    ListOfColumns = new List<ColumnInfo>();
                    for (int i = 0; i < FirstFullRow.Length; i++)
                        ListOfColumns.Add(new ColumnInfo() { });
                    FirstRow = reader.ReadLine().Split(',');


                    // if first row is all strings and first full row is NOT all strings,
                    // the first line could be the header of the CSV file. I add those strings
                    // as column names
                    HasHeader = this.hasHeaderCheck.Checked;
                    setColumnNames();

                    // determine data types by looking at the actual fields
                    determineDataTypes();

                    printColumnsInTree();

                    this.richTextBox1.Clear();
                    for (int i = 0; i < FirstFullRow.Length; i++)
                    {
                        this.richTextBox1.AppendText(ListOfColumns[i].Name + " : " + FirstFullRow[i] + nl);
                    }
                }
            }
        }
        
        private void browseFolderButton_Click(object sender, EventArgs e)
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
        }// open a dialog box to select a file

        private void changeTypeButton_Click(object sender, EventArgs e)
        {
            bool boolValue;
            Int32 intValue;
            double doubleValue;
            DateTime dateValue;

            // index of the selected node
            int.TryParse(textBox1.Text, out int SelectedNodeIndex);

            int SelectedType = this.comboBox1.SelectedIndex;
            string Value = FirstFullRow[SelectedNodeIndex];  // nodes and types are in the same order
            string NodeName = ListOfColumns[SelectedNodeIndex].Name;
            string PreviousType = ListOfColumns[SelectedNodeIndex].ActualType.ToString();
            string NewType = "";
            switch (SelectedType)
            {
                case 0:
                    if (bool.TryParse(Value, out boolValue))
                        changeType(SelectedNodeIndex, typeof(bool));
                    else
                        NewType = typeof(bool).ToString();
                    break;
                case 1:
                    if (Int32.TryParse(Value, out intValue))
                        changeType(SelectedNodeIndex, typeof(int));
                    else
                        NewType = typeof(int).ToString();
                    break;
                case 2:
                    if (double.TryParse(Value, out doubleValue))
                        changeType(SelectedNodeIndex, typeof(double));
                    else
                        NewType = typeof(double).ToString();
                    break;
                case 3:
                    if (DateTime.TryParse(Value, out dateValue))
                        changeType(SelectedNodeIndex, typeof(DateTime));
                    else
                        NewType = typeof(DateTime).ToString();
                    break;
                case 4:
                    changeType(SelectedNodeIndex, typeof(string));
                    break;
                default:
                    throw new Exception("Index not listed!");
                    break;
            }
            if (!string.IsNullOrEmpty(NewType))
                MessageBox.Show($"Cannot convert {NodeName} from {PreviousType} to {NewType}!", "Error", MessageBoxButtons.OK);
        }
       
        private void columnsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int NodeIndex;
            if (e.Node.Level > 0)
                NodeIndex = e.Node.Parent.Index;
            else
                NodeIndex = e.Node.Index;

            string NewColumnName = Interaction.InputBox("Insert new name for variable", "New Name");
            if (!string.IsNullOrEmpty(NewColumnName))
            {
                ListOfColumns[NodeIndex].Name = NewColumnName;
                printColumnsInTree();
                MessageBox.Show("Name changed successfully");
            }
        } // change name on double click

        private void columnsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int NodeIndex;
            if (e.Node.Level > 0)
                NodeIndex = e.Node.Parent.Index;
            else
                NodeIndex = e.Node.Index;


            this.textBox1.Text = NodeIndex.ToString();

            this.comboBox1.Enabled = true;
        }

        private void readCSVButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                DialogResult result = MessageBox.Show("No file selected", "Select a file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK) { }
                else throw new Exception("Error!");
            }
            else
            {
                using (TextFieldParser reader = new TextFieldParser(FilePath))
                {
                    DataSet = new List<Dictionary<string, dynamic>>();

                    // skip firs row if needed
                    if (HasHeader) reader.ReadLine();

                    while (!reader.EndOfData)
                    {
                        reader.SetDelimiters(new string[] { "," });
                        reader.CommentTokens = new string[] { "#" };
                        reader.HasFieldsEnclosedInQuotes = true;

                        string[] Values = reader.ReadFields();

                        // UnitOfObservation U = new UnitOfObservation();

                        Dictionary<string, dynamic> DataPoint = new Dictionary<string, dynamic>();

                        for (int i = 0; i < FirstFullRow.Length; i++)
                        {
                            dynamic Value = myTryParse(Values[i], ListOfColumns[i].ActualType);
                            DataPoint.Add(ListOfColumns[i].Name, Value);
                        }

                        DataSet.Add(DataPoint);

                        //Type UnitType = U.GetType();
                        //FieldInfo[] UnitFields = UnitType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                        //int i = 0;

                        //foreach (FieldInfo F in UnitFields)
                        //{
                        //    if (!string.IsNullOrWhiteSpace(Values[i]))
                        //    {
                        //        Object V = Convert.ChangeType(Values[i], F.FieldType);
                        //        F.SetValue(U, V);
                        //        i += 1;
                        //    }
                        //}

                        //Units.Add(U);
                    }
                }
            }
            printDataTable();
        }


        // ------------------------ FUNCTIONS ----------------------------
        private void setColumnNames()
        {
            // if first row is all strings and first full row is NOT all strings,
            // the first line could be the header of the CSV file. I add those strings
            // as column names
            for (int i = 0; i < FirstFullRow.Length; i++)
                if (HasHeader)
                {
                    ListOfColumns[i].Name = FirstRow[i].Replace("\"", "").Trim();
                }
                else
                    ListOfColumns[i].Name = "Field_" + i;
        }

        private void determineDataTypes()
        {
            Type t;

            for (int i = 0; i < FirstFullRow.Length; i++)
            {
                t = ParseString(FirstFullRow[i]);
                ListOfColumns[i].InferredType = ListOfColumns[i].ActualType = t;
            }
        }

        private bool rowIsAllStrings(string[] row)
        {
            foreach (string value in row)
                if (ParseString(value) != typeof(string)) return false;

            return true;
        }

        private Type ParseString(string str)
        {

            bool boolValue;
            Int32 intValue;
            double doubleValue;
            DateTime dateValue;

            if (bool.TryParse(str, out boolValue))
                return boolValue.GetType();
            else if (Int32.TryParse(str, out intValue))
                return intValue.GetType();
            else if (double.TryParse(str, out doubleValue))
                return doubleValue.GetType();
            else if (DateTime.TryParse(str, out dateValue))
                return dateValue.GetType();
            else return str.GetType();

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

        private void printColumnsInTree()
        {
            int i = 0;

            this.columnsTreeView.Nodes.Clear();

            foreach (ColumnInfo T in ListOfColumns)
            {
                columnsTreeView.Nodes.Add(T.Name);
                columnsTreeView.Nodes[i].Nodes.Add("Inferred: " + T.InferredType);
                columnsTreeView.Nodes[i].Nodes.Add("Actual: " + T.ActualType);
                i++;
            }
        }

        private void changeType(int SelectedNodeIndex, Type ChosenType)
        {
            string UpdatedColumn = ListOfColumns[SelectedNodeIndex].Name;
            Type PreviousType = ListOfColumns[SelectedNodeIndex].ActualType;

            // change of type
            ListOfColumns[SelectedNodeIndex].ActualType = ChosenType;

            MessageBox.Show($"Changed {UpdatedColumn} type from {PreviousType.ToString()} to {ChosenType.ToString()} successfully");

            printColumnsInTree();
        }

        private void printDataTable()
        {
            DataTable dt = new DataTable("Dataset");
            List<DataColumn> DataColumns = new List<DataColumn>();

            foreach (ColumnInfo Column in ListOfColumns)
            {
                DataColumns.Add(dt.Columns.Add(Column.Name, Column.ActualType));
            }

            foreach (Dictionary<string, dynamic> DataPoint in DataSet)
            {
                DataRow row = dt.NewRow();

                foreach (DataColumn dc in DataColumns)
                {
                    row[dc] = DataPoint[dc.ColumnName];
                }
                dt.Rows.Add(row);
            }

            DataView View = new DataView(dt);

            this.dataGridView1.DataSource = View;
        }

        private dynamic myTryParse(string s, Type t)
        {
            if (t.Equals(typeof(bool)) && bool.TryParse(s, out bool b)) return b;
            if (t.Equals(typeof(int)) && int.TryParse(s, out int i)) return i;
            if (t.Equals(typeof(double)) && double.TryParse(s, out double d)) return d;
            if (t.Equals(typeof(DateTime)) && DateTime.TryParse(s, out DateTime dt)) return dt;

            return s;
        }
    }
}
