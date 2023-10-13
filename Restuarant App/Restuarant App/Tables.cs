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
    public partial class Tables : Form
    {
        public Tables()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewTable newTable = new NewTable(); 
            newTable.ShowDialog();
            PopulateGridView();
        }

        private void Tables_Load(object sender, EventArgs e)
        {
            PopulateGridView();
        }
        public void PopulateGridView()
        {
            dataGridView1.DataSource = null;
            try
            {

                string query = "SELECT * FROM tables";
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
                LogExceptionToDatabase(ex);
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void LogExceptionToDatabase(Exception ex)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand("INSERT INTO ErrorLog (ErrorMessage, StackTrace, FunctionName, FileName, LogTime) VALUES (@ErrorMessage, @StackTrace, @FunctionName, @FileName, @LogTime)", con);
            command.Parameters.AddWithValue("@ErrorMessage", ex.Message);
            command.Parameters.AddWithValue("@StackTrace", ex.StackTrace);
            command.Parameters.AddWithValue("@FunctionName", GetCallingMethodName()); // Get calling method name
            command.Parameters.AddWithValue("@FileName", GetFileName()); // Get file name
            command.Parameters.AddWithValue("@LogTime", DateTime.Now);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception logEx)
            {
                // Handle any exceptions that may occur during the logging operation (optional)
                Console.WriteLine("Error while logging exception: " + logEx.Message);
            }
        }

        // Helper function to extract calling method name from stack trace
        private string GetCallingMethodName()
        {
            var frames = new StackTrace(true).GetFrames();
            if (frames != null && frames.Length >= 3)
            {
                // Index 3 represents the calling method in the stack trace
                return frames[3].GetMethod().Name;
            }
            return "Unknown";
        }

        // Helper function to extract file name from stack trace
        private string GetFileName()
        {
            var frames = new StackTrace(true).GetFrames();
            if (frames != null && frames.Length >= 3)
            {
                // Index 3 represents the calling method in the stack trace
                var fileName = frames[3].GetFileName();
                if (fileName != null)
                {
                    return System.IO.Path.GetFileName(fileName);
                }
            }
            return "Unknown";
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
                DialogResult result = MessageBox.Show("Are you sure you want to in-active this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand("UPDATE tables SET Active = 0 WHERE ID = @ID", con);
                    command.Parameters.AddWithValue("@ID", idToDelete);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            PopulateGridView();
                        }
                        else
                        {
                            // Handle the case where the record was not found in the database
                            MessageBox.Show("Record Not Found or Not Updated.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that may occur during the database operation
                        LogExceptionToDatabase(ex);
                        MessageBox.Show("Error: " + ex.Message);
                    }


                }
            }



            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "EDIT")
            {
                int rowIndex = e.RowIndex; // Get the clicked row index
                int seats = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Seats"].Value);
                string located = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["Located"].Value);
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
                EditTable E = new EditTable(seats,located,id);
                E.ShowDialog();
                PopulateGridView();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reservation R= new Reservation();
            R.ShowDialog();
        }
    }
}
