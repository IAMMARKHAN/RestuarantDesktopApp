using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using db2021finalprojectg_9;


namespace Restuarant_App
{
    public partial class Reservation : Form
    {
        public Reservation()
        {
            InitializeComponent();
        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            try
            {

                string query = "SELECT * FROM tableReservation Where Active=@C";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@C", true);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Remove")
            {
                int rowIndex = e.RowIndex; // Get the clicked row index
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand("UPDATE tableReservation SET Active = 0 WHERE Id = @ID", con);
                command.Parameters.AddWithValue("@ID", id);

              
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        dataGridView1.DataSource = null;
                        try
                        {

                            string query1 = "SELECT * FROM tableReservation Where Active=@C";
                            var con1 = Configuration.getInstance().getConnection();
                            SqlCommand command1 = new SqlCommand(query1, con1);
                            command1.Parameters.AddWithValue("@C",true);
                            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
                            DataTable dataTable1 = new DataTable();
                            adapter1.Fill(dataTable1);

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Record Not Found or Not Updated.");
                    }
                }
        }
    }
}
