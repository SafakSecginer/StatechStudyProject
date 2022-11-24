using Microsoft.VisualBasic;
using StatechStudyProject.Models;
using System.CodeDom;
using System.Windows.Forms.VisualStyles;

namespace StatechStudyProject
{
    public partial class Form1 : Form
    {
        static string resourcesPath = @"..\..\..\Resources\";
        string initialDataPath = Path.Combine(Environment.CurrentDirectory, resourcesPath, "InitialData.csv");
        string streamDataPath = Path.Combine(Environment.CurrentDirectory, resourcesPath, "StreamData.csv");
        private Thread thread;
        private List<DataModel> streamDataList;
        
        enum gridViewColumns
        {
            Alýþ,
            Fiyat,
            Satýþ
        };
        
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadInitialData();
            streamDataList = LoadStreamCSV(streamDataPath);
            CreateThread();
        }

        private void CreateThread()
        {
            thread = new Thread(CheckStreamData);
            thread.Name = "StreamDataThread";
            thread.Start();
        }

        private void CheckStreamData()
        {
            System.Diagnostics.Debug.WriteLine("Stream Data Thread is Running...");

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            try
            {
                foreach (DataModel data in streamDataList)
                {
                    System.Diagnostics.Debug.WriteLine(data.Price);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (data.Price.Equals(row.Cells[(int)gridViewColumns.Fiyat].Value))
                        {
                            if (!data.AskQuantity.Equals("0"))
                            {
                                Thread.Sleep(Int32.Parse(data.TimeDifference));
                                row.Cells[(int)gridViewColumns.Satýþ].Value = data.AskQuantity;
                            } else if (!data.BidQuantity.Equals("0"))
                            {
                                Thread.Sleep(Int32.Parse(data.TimeDifference));
                                row.Cells[(int)gridViewColumns.Alýþ].Value = data.BidQuantity;
                            }
                        }
                    }
                }
            }
            catch { }

        }

        private void LoadInitialData()
        {
            List<DataGridModel> dataGridList = LetDataListToSnapshot(LoadInitializeCSV(initialDataPath));
            dataGridView1.DataSource = dataGridList;
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
                             Alýþ = "",
                             Fiyat = data.Price,
                             Satýþ = data.Quantity
                         };

            var query2 = from data in dataModelList
                         from side in data.Side
                         where side == 'B'
                         select new DataGridModel
                         {
                             Alýþ = data.Quantity,
                             Fiyat = data.Price,
                             Satýþ = ""
                         };
            
            List<DataGridModel> sList = query1.ToList<DataGridModel>();
            List<DataGridModel> bList = query2.ToList<DataGridModel>();
            sList.AddRange(bList);
            return sList;
        }

        private void btnNewForm_Click(object sender, EventArgs e)
        {
            Form newForm = new Form1();
            newForm.Show();
        }

    }

}