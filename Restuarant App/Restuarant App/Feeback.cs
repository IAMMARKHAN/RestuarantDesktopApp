using db2021finalprojectg_9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_App
{
    public partial class Feeback : Form
    {
        public Feeback()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                    }

        private void Feeback_Load(object sender, EventArgs e)
        {
            PopulateGrid();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void PopulateGrid()
        {
            dataGridView2.DataSource = null;
            try
            {
                string query = "SELECT CustomerName, Suggesstion, CreatedAt FROM suggestion";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Time", typeof(TimeSpan));
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime createdAt = Convert.ToDateTime(row["CreatedAt"]);
                    row["Date"] = createdAt.Date;
                    row["Time"] = createdAt.TimeOfDay;
                }

                dataTable.Columns.Remove("CreatedAt");

                dataGridView2.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
