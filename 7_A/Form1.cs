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

        //------------------ USEFUL STUFF-----------------------------------------------
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
                    FirstRow = reader.ReadFields();


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
                        changeType(SelectedNodeIndex, typeof(int?));
                    else
                        NewType = typeof(int).ToString();
                    break;
                case 2:
                    if (double.TryParse(Value, out doubleValue))
                    {
                        //if (Value.Contains("."))
                        //    Value.Replace(".", ",");
                        changeType(SelectedNodeIndex, typeof(double));
                    }
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
                    reader.SetDelimiters(new string[] { "," });
                    reader.CommentTokens = new string[] { "#" };
                    reader.HasFieldsEnclosedInQuotes = true;

                    DataSet = new List<Dictionary<string, dynamic>>();

                    // skip firs row if needed
                    if (HasHeader) reader.ReadLine();

                    while (!reader.EndOfData)
                    {
                        string[] Values = reader.ReadFields();

                        Dictionary<string, dynamic> DataPoint = new Dictionary<string, dynamic>();

                        for (int i = 0; i < FirstFullRow.Length; i++)
                        {
                            dynamic Value = 0;

                            if (string.IsNullOrEmpty(Values[i])) 
                            {
                                if (ListOfColumns[i].ActualType == typeof(double))
                                    Value = 0.0;
                                else if (ListOfColumns[i].ActualType == typeof(int))
                                    Value = 0;
                                else if (ListOfColumns[i].ActualType == typeof(string))
                                    Value = "";
                            }
                            else
                            {
                                Value = myTryParse(Values[i], ListOfColumns[i].ActualType);
                            }
                            
                            DataPoint.Add(ListOfColumns[i].Name, Value);
                        }

                        DataSet.Add(DataPoint);
                    }
                }
            }
            printDataTable();

            // adds double or integer columns in combobox
            addColumnsForMean();
        }




        // ------------------------ FUNCTIONS ----------------------------
        private void addColumnsForMean()
        {
            this.columnsForMeanCombobox.Enabled = true;

            foreach (ColumnInfo C in ListOfColumns)
            {
                if (C.ActualType == typeof(int) || C.ActualType == typeof(double))
                {
                    this.columnsForMeanCombobox.Items.Add(C.Name);
                }
            }
        }

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
            if (bool.TryParse(str, out bool boolValue))
                return boolValue.GetType();
            else if (int.TryParse(str, out int intValue))
                return typeof(int?);
            else if (double.TryParse(str, out double doubleValue))
                return doubleValue.GetType();
            else if (DateTime.TryParse(str, out DateTime dateValue))
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
            if (t.Equals(typeof(int)))
            {
                if (s.Contains("."))
                    s = s.Replace(".", ",");

                if (int.TryParse(s, out int n))
                    return n;
                else
                    return null;
            }
            if (t.Equals(typeof(double))) 
            {
                if (s.Contains("."))
                    s = s.Replace(".", ",");

                if (double.TryParse(s, out double d))
                    return d;
                else
                    return double.NaN;
            }
            if (t.Equals(typeof(DateTime)) && DateTime.TryParse(s, out DateTime dt)) return dt;

            return s;
        }

        private void computeMeanButton_Click(object sender, EventArgs e)
        {
            // get column index
            string ColumnName = this.columnsForMeanCombobox.SelectedItem.ToString();
            if (string.IsNullOrEmpty(ColumnName))
            {
                DialogResult result = MessageBox.Show("No columm selected", "Select a column", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK) { }
                else throw new Exception("Error!");
            }
            else
            {
                // get values
                List<double> Values = new List<double>();

                foreach (Dictionary<string, dynamic> DataPoint in DataSet)
                {
                    Values.Add((double)DataPoint[ColumnName]);
                }

                // compute mean
                double Mean = computeOnlineMean(Values);

                // print mean
                this.richTextBox2.Text = Mean.ToString();
            }
        }

        // --------------- STAT FUNCTIONS ---------------

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    // define starting point and step
        //    double StartingPoint = 3.0;
        //    double Step = 1.0;

        //    this.richTextBox3.Clear();
        //    // computing and printing frequency distribution
        //    List<Interval> FrequencyDistribution = new List<Interval>();
        //    FrequencyDistribution = UnivariateDistribution_CountinuousVariable(ListOfAthletes.Select((Athlete a) => a.FinishingTime).ToList(), StartingPoint, Step);

        //    printFrequencyDistributionInterval(FrequencyDistribution);
        //}

        //private List<Interval> UnivariateDistribution_CountinuousVariable(List<double> L, double StartingPoint, double Step)
        //{

        //    List<Interval> ListOfIntervals = new List<Interval>();

        //    // Crate and insert first interval
        //    Interval Interval0 = new Interval();
        //    Interval0.LowerInclusiveBound = StartingPoint;
        //    Interval0.Step = Step;
        //    ListOfIntervals.Add(Interval0);

        //    // insertion of values
        //    foreach (var d in L)
        //    {
        //        bool ValueInserted = false;
        //        // if it's in one of the existing intervals, insert it there
        //        foreach (var I in ListOfIntervals)
        //        {
        //            if (I.containsValue(d))
        //            {
        //                I.Count += 1;
        //                ValueInserted = true;
        //                break;
        //            }
        //        }
        //        if (ValueInserted != true)
        //        {
        //            // if it's less than the lower bound of the first, add new suitable interval in the beginning
        //            if (d < ListOfIntervals[0].LowerInclusiveBound)
        //            {
        //                // we keep inserting intervals until one can accept the value
        //                while (ValueInserted != true)
        //                {
        //                    Interval I = new Interval();
        //                    I.LowerInclusiveBound = ListOfIntervals[0].LowerInclusiveBound - Step;
        //                    I.Step = Step;

        //                    if (I.containsValue(d))
        //                    {
        //                        ValueInserted = true;
        //                        I.Count += 1;
        //                    }

        //                    ListOfIntervals.Insert(0, I);
        //                }
        //            }
        //            else if (d >= (ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step))
        //            {
        //                // we keep inserting intervals until one can accept the value
        //                while (ValueInserted != true)
        //                {
        //                    Interval I = new Interval
        //                    {
        //                        LowerInclusiveBound = ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step,
        //                        Step = Step
        //                    };

        //                    if (I.containsValue(d))
        //                    {
        //                        ValueInserted = true;
        //                        I.Count += 1;
        //                    }

        //                    ListOfIntervals.Add(I);
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("Not Accepted value");
        //            }
        //        }
        //    }

        //    // set relative frequencies and percentages
        //    foreach (var I in ListOfIntervals)
        //    {
        //        I.RelativeFrequency = (double)I.Count / L.Count();
        //        I.Percentage = I.RelativeFrequency * 100;

        //    }

        //    return ListOfIntervals;
        //}

        private double computeOnlineMean(List<double> L)
        {
            // Computation of arithmetic meanusing the Knuth Formula

            double avg = 0;
            int i = 0;
            foreach (var d in L)
            {
                // update avg value
                i += 1;
                avg += (d - avg) / i;
            }

            return avg;
        }

        //private void printFrequencyDistributionInterval(List<Interval> L)
        //{
        //    double tot = 0;
        //    int count = 0;

        //    this.richTextBox3.AppendText("Finishing time".PadRight(16) +
        //                                    "Num of Ath".PadRight(16) +
        //                                    "Rel Freq".PadRight(16) +
        //                                    "Perc".PadRight(16) + nl);
        //    this.richTextBox3.AppendText("_______________________" + nl);

        //    foreach (var I in L)
        //    {
        //        this.richTextBox3.AppendText($"[{I.LowerInclusiveBound}h - " +
        //            $"{I.LowerInclusiveBound + I.Step}h]  --> ".PadRight(16) +
        //            $"{I.Count}".PadRight(16) +
        //            $"{I.RelativeFrequency:0.##}".PadRight(16) +
        //            $"{I.Percentage:##.##} %".PadRight(16) + nl);
        //        tot += I.RelativeFrequency;
        //        count += I.Count;
        //    }
        //    this.richTextBox3.AppendText($"Sum of relative frequencies: {tot}" + nl);
        //    this.richTextBox3.AppendText($"Total units: {count}");
        //}
    }
}
