using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Globalization;

namespace CSV_Reader
{
    public partial class Form1 : Form
    {
        private OpenFileDialog ofd = new OpenFileDialog();
        private string firstline = null;
        private string secondline = null;
        private bool isRead = false;
        private bool hasHeader = false;
        private string path = null;
        private Dictionary<string, Type> valTypeDict = null;
        double STEP = 0;
        double MAX = 0;
        double MIN = 0;
        DataTable dterr = new DataTable();
        List<Interval> intervalList = null;


        public Form1()
        {
            InitializeComponent();
            ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            
        }
        private bool isCSVFile(String file)
        {
            if (file.EndsWith(".csv"))
            {
                pathTextBox.Text = file;
                return true;
            }
            else
            {
                MessageBox.Show("Select a csv file please!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pathTextBox.Text = "";
                return false;
            }

        }
        private void pathTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        private void pathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {

                isCSVFile(file);

            }
        }
        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            path = pathTextBox.Text;
            isRead = false;
            hasHeader = false;
            dataGridView.DataSource = dterr;
        }
        private void openButton_Click(object sender, EventArgs e)
        {

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                isCSVFile(ofd.FileName);

            }
        }
        private void readButton_Click(object sender, EventArgs e)
        {
            if (isCSVFile(pathTextBox.Text))
            {

                try
                {

                    using (StreamReader sr = new StreamReader(pathTextBox.Text))
                    {


                        if ((firstline = sr.ReadLine()) != null)
                        {
                            firstLineTextBox.Text = firstline;
                        }

                        if ((secondline = sr.ReadLine()) == null)
                        {
                            MessageBox.Show("Your file has only one line", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    statusLabel.Text = "Status: File read correctly";

                    isRead = true;
                }
                catch (FileLoadException exc)
                {
                    MessageBox.Show("The file cannot be read!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    throw new FileLoadException("The file cannot be read!", exc);


                }
            }
        }
        private void getVariableType(string[] varArray, dynamic[] typeArray)
        {
            valTypeDict = new Dictionary<string, Type>();

            for (int i = 0; i < varArray.Length; i++)
            {
                valTypeDict.Add(varArray[i], typeArray[i].GetType());
            }
            printTypes();
            statusLabel.Text = "Status: Types got";

        }
        private void setVariableList(string[] varArray)
        {
            foreach (string var in varArray)
            {
                variableComboBox.Items.Add(var);
            }
        }
        private void changeType()
        {
            if (integerRadioButton.Checked)
            {
                valTypeDict[variableComboBox.SelectedItem.ToString()] = typeof(int);
                statusLabel.Text = "Status: Type changed";
            }
            if (doubleRadioButton.Checked)
            {
                valTypeDict[variableComboBox.SelectedItem.ToString()] = typeof(double);
                statusLabel.Text = "Status: Type changed";

            }
            if (stringRadioButton.Checked)
            {
                valTypeDict[variableComboBox.SelectedItem.ToString()] = typeof(string);
                statusLabel.Text = "Status: Type changed";

            }
        }
        private void printTypes()
        {
            typePreviewRichTextBox.Clear();
            foreach (KeyValuePair<string, Type> kvp in valTypeDict)
            {
                typePreviewRichTextBox.AppendText(kvp.Key + ": " + kvp.Value + Environment.NewLine);
            }
        }  
        private void headerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (headerCheckBox.Checked)
            {
                generateHeaderButton.Enabled = false;
                acceptHeaderButton.Enabled = true;
            }
            else
            {
                generateHeaderButton.Enabled = true;
                acceptHeaderButton.Enabled = false;
            }
        }
        private void acceptHeaderButton_Click(object sender, EventArgs e)
        {

            if (!isRead)
            {
                MessageBox.Show("You should select a file first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            hasHeader = true;
            variableComboBox.Items.Clear();
            string[] varArray = firstLineTextBox.Text.Trim().Split(',');
            dynamic[] typeArray = secondline.Trim().Split(',');
            setVariableList(varArray);
            getVariableType(varArray, typeArray);
        }
        private void generateHeaderButton_Click(object sender, EventArgs e)
        {
            if (!isRead)
            {
                MessageBox.Show("You should select a file first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            variableComboBox.Items.Clear();
            string[] varArray = firstLineTextBox.Text.Trim().Split(',');
            dynamic[] typeArray = secondline.Trim().Split(',');
            for (int i = 1; i <= varArray.Length; i++)
            {
                varArray[i - 1] = "X" + i;
            }
            setVariableList(varArray);
            getVariableType(varArray, typeArray);
            hasHeader = false;
        }
        private void changeButton_Click(object sender, EventArgs e)
        {
            if (!isRead)
            {
                MessageBox.Show("You should select a file first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            if ((!integerRadioButton.Checked && !doubleRadioButton.Checked && !stringRadioButton.Checked) || variableComboBox.SelectedItem == null)
            {
                MessageBox.Show("You should select a variable and a type first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            changeType();
            printTypes();

        }
        private void importButton_Click(object sender, EventArgs e)
        {
            if (!isRead)
            {
                MessageBox.Show("You should select a file first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            computationComboBox.Items.Clear();

            printTable();

            foreach (KeyValuePair<String, Type> kvp in valTypeDict)
            {
                if (kvp.Value == typeof(int) || kvp.Value == typeof(double))
                {
                    computationComboBox.Items.Add(kvp.Key);
                }
            }

        }
        private void printTable()
        {
            string line;
            
            DataTable dt = new DataTable();

            using (StreamReader sr = new StreamReader(pathTextBox.Text))
            {



                try
                {

                    if (hasHeader)
                    {
                        if ((line = sr.ReadLine()) != null)
                        {
                            string[] header = line.Trim().Split(',');
                            foreach (string h in header) dt.Columns.Add(h);

                        }

                    }
                    else
                    {

                        if ((line = sr.ReadLine()) != null)
                        {
                            string[] values = line.Trim().Split(',');
                            int len = line.Trim().Split(',').Length;

                            for (int i = 0; i < len; i++)
                            {
                                dt.Columns.Add("X" + (i + 1));
                            }

                            DataRow firstrow = dt.NewRow();
                            int ind = 0;
                            foreach (DataColumn h in dt.Columns)
                            {
                                try
                                {
                                    Type type = valTypeDict[h.ToString()];
                                    if (type != values[ind].GetType())
                                    {
                                        firstrow[h] = Convert.ChangeType(values[ind], type, CultureInfo.InvariantCulture);
                                    }
                                    else firstrow[h] = values[ind];
                                }
                                catch (FormatException exc)
                                {
                                    statusLabel.Text = "A parsing error occourred parsing " + h + " to: " + valTypeDict[h.ToString()];
                                    dataGridView.DataSource = dterr;

                                    return; ;
                                }


                                ind++;

                            }
                            dt.Rows.Add(firstrow);

                        }
                    }

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Trim().Split(',');
                        DataRow row = dt.NewRow();
                        int ind = 0;


                        foreach (DataColumn h in dt.Columns)
                        {
                            try
                            {
                                Type type = valTypeDict[h.ToString()];
                                if (type != values[ind].GetType())
                                {
                                    row[h] = Convert.ChangeType(values[ind], type, CultureInfo.InvariantCulture);
                                }
                                else row[h] = values[ind];
                            }
                            catch (FormatException exc)
                            {
                                statusLabel.Text = "A parsing error occourred parsing " + h + " to: " + valTypeDict[h.ToString()];
                                dataGridView.DataSource = dterr;
                                return;
                            }
                            ind++;

                        }
                        dt.Rows.Add(row);
                    }
                }
                catch (FileLoadException exc)
                {
                    MessageBox.Show("The file cannot be read!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    throw new FileLoadException("The file cannot be read!", exc);


                }
                dataGridView.DataSource = dt;
            }
            statusLabel.Text = "Status: File correctly imported and parsed";
        }
        private void dataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentcell = dataGridView.CurrentCell;
            try
            {
                string columnName = dataGridView.Columns[currentcell.ColumnIndex].Name;
                Type type = valTypeDict[columnName];
                if (type != currentcell.Value.GetType())
                {
                    currentcell.Value = Convert.ChangeType(currentcell.Value, type, CultureInfo.InvariantCulture);
                }

            }
            catch (FormatException)
            {
                statusLabel.Text = "Status: Illegal Format";
                dataGridView.CancelEdit();
                dataGridView.RefreshEdit();


                return;
            }
            catch (StackOverflowException)
            {
                statusLabel.Text = "Status: Illegal Format";
                dataGridView.CancelEdit();
                dataGridView.RefreshEdit();

                return;
            }
            catch (InvalidCastException)
            {
                statusLabel.Text = "Status: Illegal Format";
                dataGridView.CancelEdit();
                dataGridView.RefreshEdit();

                return;
            }
        }
        private void computationButton_Click(object sender, EventArgs e)
        {
            if (!isRead)
            {
                MessageBox.Show("You should select a file first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            if (computationComboBox.SelectedItem == null)
            {
                MessageBox.Show("You should select a variable first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            if (stepTextBox.Text == "" || !double.TryParse(stepTextBox.Text, out _))
            {
                MessageBox.Show("Digit the width of the intervals, please", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }
            else STEP = Convert.ToDouble(stepTextBox.Text, CultureInfo.InvariantCulture);
            computationRichTextBox.Clear();
            
            

            List<double> valueList = new List<double>();
            
            Type type = valTypeDict[computationComboBox.SelectedItem.ToString()];

            
            //dati colonna
            foreach (DataGridViewColumn c in dataGridView.Columns)
            {
                if (c.Name == computationComboBox.SelectedItem.ToString())
                {
                    foreach (DataGridViewRow r in dataGridView.Rows)
                    {
                        if ((type = valTypeDict[c.Name]) == typeof(int))
                        {
                            valueList.Add(Convert.ToInt32(dataGridView.Rows[r.Index].Cells[c.Index].Value, CultureInfo.InvariantCulture));
                        }
                        else valueList.Add(Convert.ToDouble(dataGridView.Rows[r.Index].Cells[c.Index].Value,  CultureInfo.CurrentCulture));


                    }
                }
            }
            valueList.RemoveAt(valueList.Count - 1);
            if ((valueList.Max() / STEP) > 1000)
            {
                
                var answer=MessageBox.Show("The width is too small, the computation could last several time. Would you like to change it?", "Warning", MessageBoxButtons.YesNo,  MessageBoxIcon.Warning);
                if (answer == DialogResult.Yes) return;
            }

            knuthAverage(valueList);
            computationRichTextBox.AppendText("=====================================" + Environment.NewLine);
            distribution(valueList);
            statusLabel.Text = "Status: Computation Completed";



        }
        private void knuthAverage(List<double> valueList)
        {
            double avg = 0;
            for (int n = 1, i = 0; i < valueList.Count; i++, n++)
            {
                double v = valueList[i];
                avg = avg + ((v - avg) / n);

            }
            computationRichTextBox.AppendText("The Average is: " + avg.ToString("0.00")+Environment.NewLine);
            
        }
        private void distribution(List<double> valuelist)
        {
            
            int len = valuelist.Count;
            MAX = valuelist.Max();
            MIN = valuelist.Min();
            
            MIN = MIN - (MIN % STEP);
            MAX = MAX + STEP - (MAX % STEP);
            generateIntervals();
            foreach (double v in valuelist)
            {
                foreach( Interval i in intervalList)
                {
                    if (v >= i.LOWER && v < i.UPPER)
                    {
                        i.COUNT++;
                        break;
                    }
                }
            }
            foreach (Interval i in intervalList)
            {
                computationRichTextBox.AppendText(("[" + i.LOWER + " ; " + i.UPPER + "): " + i.COUNT).PadRight(35)+Environment.NewLine);
            }            



        }
        private void generateIntervals()
        {
            intervalList = new List<Interval>();
            for (double i=MIN; i<MAX;  i += STEP)
            {
                Interval interval = new Interval(i, i + STEP);
                intervalList.Add(interval);
            }
        }
        private class Interval {
            public double LOWER = 0;
            public double UPPER = 0;
            public int COUNT = 0;
            public Interval(double min, double max) {
                LOWER = min;
                UPPER= max;
                COUNT = 0;
                
            }
           

        }
    }
}
