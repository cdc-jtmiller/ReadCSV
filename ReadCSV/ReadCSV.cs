using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NReco.Csv;

namespace ReadCSV
{
    public partial class ReadCSV : Form
    {
        public ReadCSV()
        {
            InitializeComponent();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            try
            {
                string sFileName = "";
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
                OpenFileDialog sDialogFName = new OpenFileDialog();
                sDialogFName.Title = "Open CSV file";
                sDialogFName.Filter = "CSV Files (*.csv)|*.csv";
                if (sDialogFName.ShowDialog() == DialogResult.OK)
                {
                    sFileName = sDialogFName.FileName;
                    ImportCsvFile(sFileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data : " + ex.Message);
            }
        }


        public void ImportCsvFile(string filename)
        {
            using (StreamReader sr= new StreamReader(filename))
            {
                DataTable netData = new DataTable();
                string[] headers = sr.ReadLine().Split(',');
                var csvReader = new CsvReader(sr, ",");
                for (int i = 0; i < headers.Count(); i++)
                {
                    netData.Columns.Add(headers[i], typeof(string));
                }

                {
                    while (csvReader.Read())
                    {
                        DataRow dr = netData.NewRow();
                        for (int i = 0; i < csvReader.FieldsCount; i++)
                        {
                            dr[i] = csvReader[i];
                        }
                        netData.Rows.Add(dr);
                    }
                    dataGridView1.DataSource = netData;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
