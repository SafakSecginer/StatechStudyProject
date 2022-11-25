using Microsoft.VisualBasic;
using StatechStudyProject.Models;
using System.CodeDom;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.DataFormats;

namespace StatechStudyProject
{
    public partial class Form1 : Form
    {

        private Thread thread;
        private int localUpdatedDataIndex = 0;

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

            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            try
            {
                foreach (DataModel data in Constants.streamDataList)
                {
                    
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        new Thread(() => dataGridView1.Invalidate());
                        if (data.Price.Equals(row.Cells[(int)gridViewColumns.Fiyat].Value))
                        {
                            
                            if (localUpdatedDataIndex >= Constants.updatedDataIndex)
                            {
                                if (!data.AskQuantity.Equals("0"))
                                {
                                    
                                    row.Cells[(int)gridViewColumns.Satýþ].Style.BackColor = Color.Gray;

                                    Thread.Sleep(Int32.Parse(data.TimeDifference));

                                    row.Cells[(int)gridViewColumns.Satýþ].Style.BackColor = Color.White;

                                    var found = Constants.dataGridList.FirstOrDefault(c => c.Fiyat == data.Price);
                                    found.Satýþ = data.AskQuantity;

                                    Constants.updatedDataIndex++;

                                }
                                else if (!data.BidQuantity.Equals("0"))
                                {
                                    
                                    row.Cells[(int)gridViewColumns.Alýþ].Style.BackColor = Color.Gray;

                                    Thread.Sleep(Int32.Parse(data.TimeDifference));

                                    row.Cells[(int)gridViewColumns.Alýþ].Style.BackColor = Color.White;

                                    var found = Constants.dataGridList.FirstOrDefault(c => c.Fiyat == data.Price);
                                    found.Alýþ = data.BidQuantity;

                                    Constants.updatedDataIndex++;

                                    //row.Cells[(int)gridViewColumns.Alýþ].Value = data.BidQuantity;

                                }
                            }
                            localUpdatedDataIndex++;
                        }
                        
                    }
                    }
                }
            catch { }

        }

    }

}