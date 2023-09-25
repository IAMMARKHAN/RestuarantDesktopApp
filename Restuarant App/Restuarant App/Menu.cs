using db2021finalprojectg_9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Restuarant_App
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //To View All In DataGridView
            PopulateDataGridView();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewMenu M = new NewMenu();
            M.ShowDialog();
        }
        public void PopulateDataGridView()
        {
            // Replace with your connection string

            try
            {
                string query = "SELECT Id,Name,Category,Price,Size FROM menu";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the delete button is clicked (assuming the column name is "DELETE")
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "DELETE")
            {
                // Assuming you have an ID column in your DataGridView (change the column index accordingly)
                int rowIndex = e.RowIndex; // Get the clicked row index
                int idToDelete = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);

                // Ask for confirmation
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand("DELETE FROM menu WHERE Id = @ID", con);
                    command.Parameters.AddWithValue("@ID", idToDelete);
                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Successfully deleted from the database
                            // You can also remove the row from the DataGridView if needed
                            dataGridView1.Rows.RemoveAt(rowIndex);
                        }
                        else
                        {
                            // Handle the case where the record was not found in the database
                            MessageBox.Show("Record Not Found or Not Deleted.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that may occur during the database operation
                        MessageBox.Show("Error: " + ex.Message);
                    }

                }
            }

            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "EDIT")
            {
                int rowIndex = e.RowIndex; // Get the clicked row index
                string name = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["Name"].Value);
                string category = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["Category"].Value);
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
                int price = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Price"].Value);
                string size = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["Size"].Value);
                EditMenu E = new EditMenu(name, category, id,price,size);
                E.ShowDialog();
            }
        }
    }
}
