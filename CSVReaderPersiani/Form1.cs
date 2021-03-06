﻿using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace CSVReaderPersiani
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            initGraphics();
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

        // RealWorld Window
        public double MinX_Win;
        public double MinY_Win;
        public double MaxX_Win;
        public double MaxY_Win;
        public double Range_X;
        public double Range_Y;

        Rectangle ViewPort;
        Bitmap b;
        Bitmap b2;
        Graphics g;
        Graphics g2;

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

                    // print a line for preview
                    this.richTextBox1.Clear();
                    for (int i = 0; i < FirstFullRow.Length; i++)
                    {
                        this.richTextBox1.AppendText(ListOfColumns[i].Name + " : " + FirstFullRow[i] + nl);
                    }

                    // enable buttons
                    this.readCSVButton.Enabled = true;
                    this.changeTypeButton.Enabled = true;
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

            int SelectedType = this.typesComboBox.SelectedIndex;
            string Value = FirstFullRow[SelectedNodeIndex];  // nodes and types are in the same order
            string NodeName = ListOfColumns[SelectedNodeIndex].Name;
            string PreviousType = ListOfColumns[SelectedNodeIndex].ActualType.ToString();
            string NewType = "";
            switch (SelectedType)
            {
                case -1:
                    break;
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

            this.typesComboBox.Enabled = true;
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

                    // skip first row if needed
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
                                    Value = DBNull.Value;
                                else if (ListOfColumns[i].ActualType == typeof(int))
                                    Value = DBNull.Value;
                                else if (ListOfColumns[i].ActualType == typeof(string))
                                    Value = DBNull.Value;
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

            this.computeMeanButton.Enabled = true;
            this.computeFrequencyDistribution.Enabled = true;
            this.printChartButton.Enabled = true;
            this.columnsForChart1.SelectedItem = "";
            this.columnsForChart2.SelectedItem = "";

            // adds double or integer columns in combobox
            addColumnsForMean();
        }




        // ------------------------ FUNCTIONS ----------------------------
        private void addColumnsForMean()
        {
            this.columnsForMeanCombobox.Items.Clear();
            this.columnsForFrequencyDistribution.Items.Clear();
            this.columnsForChart1.Items.Clear();
            this.columnsForChart2.Items.Clear();
            // enable buttons
            this.computeMeanButton.Enabled = true;
            this.computeFrequencyDistribution.Enabled = true;
            this.printChartButton.Enabled = true;
            this.columnsForChart1.SelectedItem = "";
            this.columnsForChart2.SelectedItem = "";

            foreach (ColumnInfo C in ListOfColumns)
            {
                if (C.ActualType == typeof(int) || C.ActualType == typeof(double))
                {
                    this.columnsForMeanCombobox.Items.Add(C.Name);
                    this.columnsForFrequencyDistribution.Items.Add(C.Name);
                    this.columnsForChart1.Items.Add(C.Name);
                    this.columnsForChart2.Items.Add(C.Name);
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
                return typeof(int);
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
            this.dataGridView2.DataSource = View;
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
                    return DBNull.Value;
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
            if (this.columnsForMeanCombobox.SelectedIndex <= -1)
            {
                DialogResult result = MessageBox.Show("No columm selected", "Select a column", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK) { }
                else throw new Exception("Error!");
            }
            else
            {
                string ColumnName = this.columnsForMeanCombobox.SelectedItem.ToString();
                // get values
                List<double> Values = new List<double>();

                foreach (Dictionary<string, dynamic> DataPoint in DataSet)
                {
                    if (DataPoint[ColumnName].GetType() == typeof(System.DBNull))
                        Values.Add(double.NaN);
                    else
                        Values.Add((double)DataPoint[ColumnName]);
                }

                // compute mean
                double Mean = computeOnlineMean(Values);

                // print mean
                this.richTextBox2.Text = Mean.ToString();
            }
        }

        // --------------- STAT FUNCTIONS ---------------

        private void computeFrequencyDistribution_Click(object sender, EventArgs e)
        {
            // define starting point and step
            double StartingPoint = (double)this.numericUpDownStartingPoint.Value;
            double Step = (double)this.numericUpDownStep.Value;

            string ColumnName = this.columnsForFrequencyDistribution.Text;
            if (string.IsNullOrEmpty(ColumnName))
            {
                return;
            }

            this.richTextBox4.Clear();
            // computing and printing frequency distribution
            List<Interval> FrequencyDistribution = new List<Interval>();
            FrequencyDistribution = UnivariateDistribution_CountinuousVariable(DataSet.Select(D => (Convert.IsDBNull(D[ColumnName]) ? double.NaN : (double)D[ColumnName])).ToList(), StartingPoint, Step);

            printFrequencyDistributionInterval(FrequencyDistribution);
        }

        private List<Interval> UnivariateDistribution_CountinuousVariable(List<double> L, double StartingPoint, double Step)
        {

            List<Interval> ListOfIntervals = new List<Interval>();

            // Crate and insert first interval
            Interval Interval0 = new Interval();
            Interval0.LowerInclusiveBound = StartingPoint;
            Interval0.Step = Step;
            ListOfIntervals.Add(Interval0);

            // insertion of values
            foreach (var d in L)
            {
                if (double.IsNaN(d))
                    continue;

                bool ValueInserted = false;
                // if it's in one of the existing intervals, insert it there
                foreach (var I in ListOfIntervals)
                {
                    if (I.containsValue(d))
                    {
                        I.Count += 1;
                        ValueInserted = true;
                        break;
                    }
                }
                if (ValueInserted != true)
                {
                    // if it's less than the lower bound of the first, add new suitable interval in the beginning
                    if (d < ListOfIntervals[0].LowerInclusiveBound)
                    {
                        // we keep inserting intervals until one can accept the value
                        while (ValueInserted != true)
                        {
                            Interval I = new Interval();
                            I.LowerInclusiveBound = ListOfIntervals[0].LowerInclusiveBound - Step;
                            I.Step = Step;

                            if (I.containsValue(d))
                            {
                                ValueInserted = true;
                                I.Count += 1;
                            }

                            ListOfIntervals.Insert(0, I);
                        }
                    }
                    else if (d >= (ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step))
                    {
                        // we keep inserting intervals until one can accept the value
                        while (ValueInserted != true)
                        {
                            Interval I = new Interval
                            {
                                LowerInclusiveBound = ListOfIntervals[ListOfIntervals.Count - 1].LowerInclusiveBound + Step,
                                Step = Step
                            };

                            if (I.containsValue(d))
                            {
                                ValueInserted = true;
                                I.Count += 1;
                            }

                            ListOfIntervals.Add(I);
                        }
                    }
                    else
                    {
                        throw new Exception("Not Accepted value");
                    }
                }
            }

            // set relative frequencies and percentages
            foreach (var I in ListOfIntervals)
            {
                I.RelativeFrequency = (double)I.Count / L.Count();
                I.Percentage = I.RelativeFrequency * 100;

            }

            return ListOfIntervals;
        }

        private double computeOnlineMean(List<double> L)
        {
            // Computation of arithmetic meanusing the Knuth Formula

            double avg = 0;
            int i = 0;
            foreach (var d in L)
            {
                if (double.IsNaN(d))
                    continue;
                // update avg value
                i += 1;
                avg += (d - avg) / i;
            }

            return avg;
        }

        private void printFrequencyDistributionInterval(List<Interval> L)
        {
            double tot = 0;
            int count = 0;

            this.richTextBox4.AppendText("Value".PadLeft(16) +
                                            "Num".PadLeft(16) +
                                            "Rel Freq".PadLeft(16) +
                                            "Perc".PadLeft(16) + nl);
            this.richTextBox4.AppendText("_______________________" + nl);

            foreach (var I in L)
            {
                this.richTextBox4.AppendText($"[{I.LowerInclusiveBound} - " +
                    $"{I.LowerInclusiveBound + I.Step})     ".PadLeft(16) +
                    $"{I.Count}".PadLeft(16) +
                    $"{I.RelativeFrequency:0.##}".PadLeft(16) +
                    $"{I.Percentage:##.##} %".PadLeft(16) + nl);
                tot += I.RelativeFrequency;
                count += I.Count;
            }
            this.richTextBox4.AppendText($"Sum of relative frequencies: {tot}" + nl);
            this.richTextBox4.AppendText($"Total units: {count}");
        }

        private double computeOnlineVariance(List<double> L)
        {
            double SSn, avg_act, avg_prev;
            SSn = avg_prev = avg_act = 0.0;

            for (int i = 1; i <= L.Count; i++)
            {
                avg_prev = computeOnlineMean(L.Take(i - 1).ToList());
                avg_act = computeOnlineMean(L.Take(i).ToList());

                SSn = SSn + (L[i-1] - avg_prev) * (L[i-1] - avg_act);
            }

            return (SSn / L.Count);
        }

        private double computeOnlineCovariance (List<double> Y, List<double> X)
        {
            double SCn, avg_act_x, avg_prev_y;
            SCn = avg_prev_y = avg_act_x = 0.0;
            //List<double> XColumn = D.Select(row => row[0]).ToList();
            //List<double> YColumn = D.Select(row => row[1]).ToList();

            for (int i = 1; i <= Y.Count; i++)
            {
                avg_prev_y = computeOnlineMean(Y.Take(i - 1).ToList());
                avg_act_x = computeOnlineMean(X.Take(i).ToList());

                SCn = SCn + (X[i - 1] - avg_act_x) * (Y[i - 1] - avg_prev_y);
            }

            return (SCn) / Y.Count;
        }


        // -------------- GRAPHIC FUNCTIONS -----------------------
        List<DataPointForChart> DataSetForChart;

        ResizableRectangle R2;

        Rectangle ViewPort_MouseDown;
        Point Click_Point_Drag;
        bool Dragging;
        bool Resizing;

        string Name_X;
        string Name_Y;

        List<Interval> FrequencyDistributionX;
        List<Interval> FrequencyDistributionY;
        private void drawChartButton_Click(object sender, EventArgs e)
        {

            if (this.columnsForChart1.SelectedIndex <= -1 || this.columnsForChart2.SelectedIndex <= -1)
                return;

            DataSetForChart = generateDataSetForChart();
            MinX_Win = DataSetForChart.Min(D => D.X);
            MinY_Win = DataSetForChart.Min(D => D.Y);
            MaxX_Win = DataSetForChart.Max(D => D.X);
            MaxY_Win = DataSetForChart.Max(D => D.Y);

            Range_X = MaxX_Win - MinX_Win;
            Range_Y = MaxY_Win - MinY_Win;

            Name_X = this.columnsForChart1.SelectedItem.ToString();
            Name_Y = this.columnsForChart2.SelectedItem.ToString();

            // frequency distributions
            double StepX = (double)this.stepX.Value;
            double StepY = (double)this.stepY.Value;
            double StartingPointX = MinX_Win;
            FrequencyDistributionX = new List<Interval>();
            FrequencyDistributionX = UnivariateDistribution_CountinuousVariable(DataSetForChart.Select(D => D.X).ToList(), StartingPointX, StepX);

            double StartingPointY = MinY_Win;
            FrequencyDistributionY = new List<Interval>();
            FrequencyDistributionY = UnivariateDistribution_CountinuousVariable(DataSetForChart.Select(D => D.Y).ToList(), StartingPointY, StepY);

            if (string.IsNullOrEmpty(Name_X) || string.IsNullOrEmpty(Name_X))
                return;

            // -------------- scatterplot --------------
            //ViewPort = new Rectangle(0, 0, 400, 400);
            R2 = new ResizableRectangle(chartPictureBox, b, g, MinX_Win, MinY_Win, MaxX_Win, MaxY_Win, new Rectangle(20, 20, 400, 400));
            R2.ModifiedRect += drawCharts;
            drawCharts();


            // -----------contingency table-----------
            g2.Clear(Color.White);

            MaxSizeX = g2.MeasureString(FrequencyDistributionX.Last().printInterval(), DefaultFont);
            MaxSizeY = g2.MeasureString(FrequencyDistributionY.Last().printInterval(), DefaultFont);
            MaxSize = MaxSizeX.Width >= MaxSizeY.Width ? MaxSizeX : MaxSizeY;

            //R = new ResizableRectangle(pictureBox1, b2, g2, 0, 0, MaxSize.Width * FrequencyDistributionX.Count, MaxSize.Height * FrequencyDistributionY.Count, new Rectangle(0, 0, 400, 400));
            R = new ResizableRectangle(pictureBox1, b2, g2, 0, 0, MaxSize.Width * FrequencyDistributionX.Count,
                MaxSize.Height * FrequencyDistributionY.Count,
                new Rectangle(0, 0,
                    (int)MaxSize.Width * FrequencyDistributionX.Count,
                    (int)MaxSize.Height * FrequencyDistributionY.Count)
                );
            pictureBox1.Image = b2;
            R.ModifiedRect += drawContingencyTable;
            drawContingencyTable();

            // ----------- correlation index ------------
            double cov_xy = computeOnlineCovariance(DataSetForChart.Select(D => D.X).ToList(), DataSetForChart.Select(D => D.Y).ToList());
            double var_X = computeOnlineVariance(DataSetForChart.Select(D => D.X).ToList());
            double var_Y = computeOnlineVariance(DataSetForChart.Select(D => D.Y).ToList());
            double r = cov_xy / Math.Sqrt(var_X * var_Y);
            this.correlationTextBox.Clear();
            this.correlationTextBox.AppendText(r.ToString("G2"));
        }

        private List<DataPointForChart> generateDataSetForChart()
        {
            string ColumnName1 = this.columnsForChart1.SelectedItem.ToString();
            string ColumnName2 = this.columnsForChart2.SelectedItem.ToString();
            List<DataPointForChart> DS = new List<DataPointForChart>();

            foreach (Dictionary<string, dynamic> D in DataSet)
            {
                if (Convert.IsDBNull(D[ColumnName1]) || Convert.IsDBNull(D[ColumnName2]))
                    continue;
                else
                {
                    DataPointForChart DP = new DataPointForChart()
                    {
                        X = (double)D[ColumnName1],
                        Y = (double)D[ColumnName2],
                    };

                    DS.Add(DP);
                }
            }

            return DS;
        }

        private void drawCharts()
        {
            g.Clear(Color.Gainsboro);
            //g.DrawRectangle(Pens.Red, ViewPort);

            drawAxis();

            drawRugPlot();

            // print two lines representing averages
            // printLinesForMean();

            drawHistogramOnAxis("X");
            drawHistogramOnAxis("Y");

            drawRegressionLine();

            drawScatterplot();

            drawQuartiles();

            chartPictureBox.Image = b;
        }

        private void drawQuartiles()
        {
            List<double> Values_X= DataSetForChart.Select(DP => DP.X).ToList();
            List<double> Values_Y= DataSetForChart.Select(DP => DP.Y).ToList();

            List<double> Quartiles_X = findQuartiles(Values_X); 
            List<double> Quartiles_Y = findQuartiles(Values_Y);

            foreach (double Q in Quartiles_Y)
            {
                R2.drawHorizontalLine("",Q, Pens.Red);
            }

            foreach (double Q in Quartiles_X)
            {
                R2.drawVerticalLine("", Q, Pens.Green);
            }
        }

        private List<double> findQuartiles(List<double> L)
        {
            List<double> Quartiles = new List<double>();
            List<double> Temp = new List<double>();
            L.Sort();

            Quartiles.Add(findMedian(L));
            for (int i = 0; i <= L.IndexOf(L[L.Count / 2]); i++)
                Temp.Add(L[i]);
            Quartiles.Insert(0, findMedian(Temp));
            Temp.Clear();
            for (int i = L.Count / 2; i <= L.LastIndexOf(L.Last()); i++)
                Temp.Add(L[i]);
            Quartiles.Add(findMedian(Temp));
            Temp.Clear();

            return Quartiles;
        }

        private double findMedian (List<double> L)
        {
            L.Sort();

            if (L.Count % 2 == 0)
            {
                int max = (int)Math.Ceiling((L.Count - 1) / 2.0);
                int min = (int)Math.Floor((L.Count - 1) / 2.0);
                return (L.ElementAt(max) + L.ElementAt(min)) / 2.0;
            } 
            else
            {
                return L.ElementAt((L.Count - 1) / 2);                
            }
            
        }

        private void drawScatterplot()
        {
            foreach (DataPointForChart D in DataSetForChart)
            {
                int X_View = R2.viewport_X(D.X);
                int Y_View = R2.viewport_Y(D.Y);

                g.FillEllipse(Brushes.Black, new Rectangle(new Point(X_View - 2, Y_View - 2), new Size(4, 4)));
                //g.DrawString(D.X.ToString() +","+D.Y.ToString(), DefaultFont, Brushes.Black, new Point((int)D.X, (int)D.Y));
            }
        }

        private void drawAxis()
        {
            Pen p = new Pen(Color.Black);
            p.EndCap = LineCap.ArrowAnchor;
            //g.DrawLine(
            //    p,
            //    ViewPort.X,
            //    ViewPort.Y + ViewPort.Height,
            //    ViewPort.X + ViewPort.Width,
            //    ViewPort.Y + ViewPort.Height);

            // X axis
            g.DrawLine(
                p,
                R2.viewport_X(MinX_Win),
                R2.viewport_Y(MinY_Win),
                R2.viewport_X(MaxX_Win),
                R2.viewport_Y(MinY_Win));

            g.DrawString(Name_X, DefaultFont, Brushes.Black,
                R2.viewport_X(MaxX_Win),
                (float)(R2.viewport_Y(MinY_Win) + 0.05*MaxY_Win));

            // Y axis
            g.DrawLine(
                p,
                R2.viewport_X(MinX_Win),
                R2.viewport_Y(MinY_Win),
                R2.viewport_X(MinX_Win),
                R2.viewport_Y(MaxY_Win)
                );

            g.DrawString(Name_Y, DefaultFont, Brushes.Black,
                R2.viewport_X(MinX_Win) - g2.MeasureString(Name_Y, DefaultFont).Width,
                (float)(R2.viewport_Y(MaxY_Win) + 0.05 * MaxX_Win));
        }

        private void drawHistogramOnAxis(string Axis)
        {
            double StepX = (double)this.stepX.Value;
            double StepY = (double)this.stepY.Value;

            if (Axis.Equals("X"))
            {
                //double StartingPointX = MinX_Win;
                //FrequencyDistributionX = new List<Interval>();
                //FrequencyDistributionX = UnivariateDistribution_CountinuousVariable(DataSetForChart.Select(D => D.X).ToList(), StartingPointX, StepX);

                // draw proportionate rectangles 
                double BarWidth = (double)R2.R.Width / FrequencyDistributionX.Count;
                double BarMaxHeight = R2.R.Height * 0.7;
                int BarNum = 0;
                foreach (Interval I in FrequencyDistributionX)
                {
                    float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
                    RectangleF R = new RectangleF(
                        (float)(R2.R.X + (BarNum * BarWidth)),
                        (float)(R2.R.Y + R2.R.Height - (BarMaxHeight * I.RelativeFrequency)),
                        (float)BarWidth,
                        BarHeight
                        );

                    // drawing a line for each interval represening the average
                    // datapoints in each interval
                    IEnumerable<double> DataPointsInInterval =
                        from D in DataSetForChart
                        where D.X >= I.LowerInclusiveBound && D.X < (I.LowerInclusiveBound + StepX)
                        select D.X;

                    double BarMean = computeOnlineMean(DataPointsInInterval.ToList());

                    PointF StartingPointForMean = new PointF(
                        (float)R2.viewport_X(BarMean),
                        R2.R.Y + R2.R.Height);

                    PointF EndingPointForMean = new PointF(
                        (float)R2.viewport_X(BarMean),
                        R2.R.Y + R2.R.Height - BarHeight);

                    SolidBrush B = new SolidBrush(Color.FromArgb(128, 0, 0, 255));
                    g.FillRectangle(B, R);
                    g.DrawLine(Pens.Brown, StartingPointForMean, EndingPointForMean);

                    BarNum++;
                }
            }
            else if (Axis.Equals("Y"))
            {
                //double StartingPointY = MinY_Win;
                //FrequencyDistributionY = new List<Interval>();
                //FrequencyDistributionY = UnivariateDistribution_CountinuousVariable(DataSetForChart.Select(D => D.Y).ToList(), StartingPointY, StepY);

                // invert list so the biggest comes before the smallest
                List<Interval> ReversedFrequencyDistributionY = Enumerable.Reverse(FrequencyDistributionY).ToList();

                // draw proportionate rectangles 
                double BarWidth = (double)R2.R.Height / ReversedFrequencyDistributionY.Count;
                double BarMaxHeight = R2.R.Width * 0.7;
                int BarNum = 0;
                foreach (Interval I in ReversedFrequencyDistributionY)
                {
                    float BarHeight = (float)(BarMaxHeight * I.RelativeFrequency);
                    RectangleF R = new RectangleF(
                        R2.R.X,
                        (float)(R2.R.Y + (BarNum * BarWidth)),
                        (float)BarHeight,
                        (float)BarWidth
                        );

                    // drawing a line for each interval represening the average
                    // datapoints in each interval
                    IEnumerable<double> DataPointsInInterval =
                        from D in DataSetForChart
                        where D.Y >= I.LowerInclusiveBound && D.Y < (I.LowerInclusiveBound + StepY)
                        select D.Y;

                    double BarMean = computeOnlineMean(DataPointsInInterval.ToList());

                    PointF StartingPointForMean = new PointF(
                        R2.R.X,
                        (float)R2.viewport_Y(BarMean)
                        );

                    PointF EndingPointForMean = new PointF(
                        R2.R.X + BarHeight,
                        (float)R2.viewport_Y(BarMean)
                        );

                    SolidBrush B = new SolidBrush(Color.FromArgb(128, 0, 200, 0));
                    g.FillRectangle(B, R);
                    g.DrawLine(Pens.Brown, StartingPointForMean, EndingPointForMean);
                    BarNum++;
                }
            }
        }

        private void initGraphics()
        {
            b = new Bitmap(this.chartPictureBox.Width, this.chartPictureBox.Height);
            g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            b2 = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g2 = Graphics.FromImage(b2);
            g2.SmoothingMode = SmoothingMode.AntiAlias;
        }

        private void drawRugPlot()
        {
            float h = (float)(0.01 * Math.Min(MaxY_Win, MaxX_Win));
            foreach (DataPointForChart D in DataSetForChart)
            {
                // X axis
                PointF StartingPointX = new PointF(
                    R2.viewport_X(D.X),
                    R2.viewport_Y(MinY_Win ) - h
                    );
                PointF EndingPointX = new PointF(
                    R2.viewport_X(D.X),
                    R2.viewport_Y(MinY_Win) + h
                    );

                g.DrawLine(Pens.Purple, StartingPointX, EndingPointX);

                // Y axis
                PointF StartingPointY = new PointF(
                    R2.viewport_X(MinX_Win - h),
                    R2.viewport_Y(D.Y)
                    );
                PointF EndingPointY = new PointF(
                    R2.viewport_X(MinX_Win + h),
                    R2.viewport_Y(D.Y)
                    );

                g.DrawLine(Pens.DarkSeaGreen, StartingPointY, EndingPointY);
            }
        }

        private void drawRegressionLine()
        {
            // y = cov(x,y)/var(x) * (x - avg_x) + avg_y
            List<double> ColX = DataSetForChart.Select(D => D.X).ToList();
            List<double> ColY = DataSetForChart.Select(D => D.Y).ToList();

            double avg_X = computeOnlineMean(ColX);
            double avg_Y = computeOnlineMean(ColY);

            double cov_xy = computeOnlineCovariance(ColX, ColY);
            double var_x = computeOnlineVariance(ColX);

            double coeff = cov_xy / var_x;

            // starting point for the line: (min_X, avg_y)
            double startingY = coeff * (MinX_Win - avg_X) + avg_Y;
            PointF StartingPoint = new PointF(
                R2.viewport_X(MinX_Win),
                R2.viewport_Y(startingY)
                );

            // ending point
            double endingY = coeff * (MaxX_Win - avg_X) + avg_Y;
            PointF EndingPoint = new PointF(
                R2.viewport_X(MaxX_Win),
                R2.viewport_Y(endingY)
                );

            Pen p = new Pen(Color.Red, 2);
            p.DashStyle = DashStyle.DashDotDot;
            g.DrawLine(p, StartingPoint, EndingPoint);

        }


        SizeF MaxSizeX;
        SizeF MaxSizeY;
        SizeF MaxSize;
        ResizableRectangle R;


        private void drawContingencyTable()
        {
            g2.Clear(Color.Gainsboro);
            // g2.DrawRectangle(Pens.Red, R.R);
            // g2.FillRectangle(Brushes.Red, R.R);
            int x;
            int y;
            bool relFreq = false;
            //List<List<int>> Matrix = generateDatasetMatrix();
            List<List<Interval>> M = generateDatasetMatrixInterval();

            if (this.relativeOrCountCheck.Checked == true)
                relFreq = true;

            // draw header for X
            for (x = 1; x <= FrequencyDistributionX.Count; x++)
            {
                g2.DrawString(FrequencyDistributionX[x - 1].printInterval(), DefaultFont, Brushes.Indigo, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win));
                // vertical lines for each column
                g2.DrawLine(Pens.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win), R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MinY_Win));
            }
            g2.DrawString("Mar Y", DefaultFont, Brushes.Indigo, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win));
            g2.DrawLine(Pens.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win), R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MinY_Win));


            for (y = 1; y <= FrequencyDistributionY.Count; y++)
            {
                // draw header for Y
                g2.DrawString(FrequencyDistributionY[y - 1].printInterval(), DefaultFont, Brushes.Black, R.viewport_X(0), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height));
                // horizontal lines for each row
                g2.DrawLine(Pens.Black, R.viewport_X(0), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height), R.viewport_X(R.R.Width), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height));

                for (x = 1; x <= FrequencyDistributionX.Count; x++)
                {
                    // joint frequency
                    //g2.DrawString(Matrix[y - 1][x - 1].ToString(), DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height));
                    if (relFreq)
                        g2.DrawString(M[y - 1][x - 1].RelativeFrequency.ToString("#.##"), DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height));
                    else
                        g2.DrawString(M[y - 1][x - 1].Count.ToString(), DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height));
                }

                
                // Marginal for Y (rightmost column)
                if (relFreq)
                    g2.DrawString(FrequencyDistributionY[y - 1].RelativeFrequency.ToString("#.##"), DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height));
                else
                    g2.DrawString(FrequencyDistributionY[y - 1].Count.ToString(), DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height));
            }
            g2.DrawString("Mar X", DefaultFont, Brushes.Indigo, R.viewport_X(0), R.viewport_Y(R.MinY_Win - MaxSize.Height));
            g2.DrawLine(Pens.Black, R.viewport_X(0), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height), R.viewport_X(R.MaxX_Win), R.viewport_Y(R.MaxY_Win - y * MaxSize.Height));
            //g2.DrawLine(Pens.Black, R.viewport_X(0), R.viewport_Y(-y * MaxSize.Height), R.viewport_X(300), R.viewport_Y(-y * MaxSize.Height));

            for (x = 1; x <= FrequencyDistributionX.Count; x++)
            {
                // Marginal for X (lowermost row)
                if (relFreq)
                    g2.DrawString(FrequencyDistributionX[x - 1].RelativeFrequency.ToString("#.##"), DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MinY_Win - MaxSize.Height));
                else
                    g2.DrawString(FrequencyDistributionX[x - 1].Count.ToString(), DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MinY_Win - MaxSize.Height));
            }

            if (relFreq)
                g2.DrawString("1", DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MinY_Win - MaxSize.Height));
            else
                g2.DrawString(DataSetForChart.Count().ToString(), DefaultFont, Brushes.Black, R.viewport_X(x * MaxSize.Width), R.viewport_Y(R.MinY_Win - MaxSize.Height));

            pictureBox1.Image = b2;
        }

        private List<List<int>> generateDatasetMatrix()
        {
            List<List<int>> M = new List<List<int>>();
            // matrix instantiation
            int LengthOfRows = FrequencyDistributionX.Count();
            for (int i = 0; i < FrequencyDistributionY.Count(); i++)
            {
                M.Add(new List<int>(new int[LengthOfRows]));
            }

            // matrix population
            foreach (DataPointForChart DP in DataSetForChart)
            {
                for (int i = 0; i < FrequencyDistributionX.Count(); i++)
                {
                    if (FrequencyDistributionX[i].containsValue(DP.X))
                    {
                        for (int j = 0; j < FrequencyDistributionY.Count(); j++)
                        {
                            if (FrequencyDistributionY[j].containsValue(DP.Y))
                                M[j][i] += 1;
                        }
                    }
                }
            }
            return M;
        }


        private List<List<Interval>> generateDatasetMatrixInterval()
        {
            List<List<Interval>> M = new List<List<Interval>>();
            // matrix instantiation
            int LengthOfRows = FrequencyDistributionX.Count();
            int tot = 0;
            for (int i = 0; i < FrequencyDistributionY.Count(); i++)
            {
                M.Add(new List<Interval>());
                for (int j = 0; j < FrequencyDistributionX.Count(); j++)
                    M[i].Add(new Interval());
            }

            // matrix population
            foreach (DataPointForChart DP in DataSetForChart)
            {
                for (int i = 0; i < FrequencyDistributionX.Count(); i++)
                {
                    if (FrequencyDistributionX[i].containsValue(DP.X))
                    {
                        for (int j = 0; j < FrequencyDistributionY.Count(); j++)
                        {
                            if (FrequencyDistributionY[j].containsValue(DP.Y))
                            {
                                M[j][i].Count += 1;
                                tot += 1;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < M.Count(); i++)
                for (int j = 0; j < M[0].Count(); j++)
                    M[i][j].RelativeFrequency = M[i][j].Count / (double)tot;

            return M;
        }
    }
}
