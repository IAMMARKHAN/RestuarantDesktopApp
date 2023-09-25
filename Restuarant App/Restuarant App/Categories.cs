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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Restuarant_App
{
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();

        }

        private void Categories_Load(object sender, EventArgs e)
        {
            PopulateDataGridView(); // Call this method to populate the DataGridView
        }
        public void PopulateDataGridView()
        {
            // Replace with your connection string

            
                try
                {

                        string query = "SELECT * FROM categories";
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand(query, con);
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

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
      
        private void button5_Click_1(object sender, EventArgs e)
        {
            NewCategory S = new NewCategory();
            S.ShowDialog();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
                    SqlCommand command = new SqlCommand("DELETE FROM categories WHERE ID = @ID", con);
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
                string type = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["Type"].Value);
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);

                EditCategory E = new EditCategory(name,type,id);
                E.ShowDialog();
            }

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
