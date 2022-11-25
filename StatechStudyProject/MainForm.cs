using StatechStudyProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatechStudyProject
{
    public partial class MainForm : Form
    {

        private Form1 form1;

        static string resourcesPath = @"..\..\..\Resources\";
        string initialDataPath = Path.Combine(Environment.CurrentDirectory, resourcesPath, "InitialData.csv");
        string streamDataPath = Path.Combine(Environment.CurrentDirectory, resourcesPath, "StreamData.csv");
        
        

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            form1 = new Form1();
            LoadInitialData();
            Constants.streamDataList = LoadStreamCSV(streamDataPath);
            
        }
        private void LoadInitialData()
        {
            Constants.dataGridList = LetDataListToSnapshot(LoadInitializeCSV(initialDataPath));
            form1.dataGridView1.DataSource = Constants.dataGridList;
        }

        public List<DataModel> LoadInitializeCSV(string csvFile)
        {
            var query = from line in File.ReadLines(csvFile).Skip(1)
                        let data = line.Split(";")
                        select new DataModel
                        {
                            Price = data[0],
                            Side = data[1],
                            Quantity = data[2]
                        };
            return query.ToList();
        }

        public List<DataModel> LoadStreamCSV(string csvFile)
        {
            var query = from line in File.ReadLines(csvFile).Skip(1)
                        let data = line.Split(";")
                        select new DataModel
                        {
                            TimeDifference = data[0],
                            BidQuantity = data[1],
                            AskQuantity = data[2],
                            Price = data[3]
                        };
            return query.ToList();

        }

        public List<DataGridModel> LetDataListToSnapshot(List<DataModel> dataModelList)
        {
            var query1 = from data in dataModelList
                         from side in data.Side
                         where side == 'S'
                         select new DataGridModel
                         {
                             Alış = "",
                             Fiyat = data.Price,
                             Satış = data.Quantity
                         };

            var query2 = from data in dataModelList
                         from side in data.Side
                         where side == 'B'
                         select new DataGridModel
                         {
                             Alış = data.Quantity,
                             Fiyat = data.Price,
                             Satış = ""
                         };

            List<DataGridModel> sList = query1.ToList<DataGridModel>();
            List<DataGridModel> bList = query2.ToList<DataGridModel>();
            sList.AddRange(bList);
            return sList;
        }

        private void btnViewDataTable_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            RefreshDatas(newForm);
            newForm.Show();
        }
        private void RefreshDatas(Form1 newForm)
        {
            var bindingSource1 = new System.Windows.Forms.BindingSource { DataSource = Constants.dataGridList };
            newForm.dataGridView1.DataSource = bindingSource1;
            newForm.dataGridView1.Invalidate();
        }

    }
}
