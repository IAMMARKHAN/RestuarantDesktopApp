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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            PopulateGrid();

        }
        public void PopulateGrid()
        {
            dataGridView1.DataSource = null;
            try
            {
                string query = "SELECT * FROM orders";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
            
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
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Pay")
            {
                int rowIndex = e.RowIndex; // Get the clicked row index
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
                Update(id);
            }
        }
        void Update(int id)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();

                // SQL update query to toggle "Active" and update "Status" accordingly based on the current value of "Active" column
                string updateQuery = "UPDATE orders SET Active = CASE WHEN Active = 1 THEN 0 ELSE 1 END, Status = CASE WHEN Active = 1 THEN 'Paid' ELSE 'Unpaid' END WHERE Id = @OrderID";

                SqlCommand updateCommand = new SqlCommand(updateQuery, con);

                updateCommand.Parameters.AddWithValue("@OrderID", id);
                updateCommand.ExecuteNonQuery();
                MessageBox.Show("Order payment status updated !");
                PopulateGrid();
            }
            catch (Exception ex)
            {
                // Handle any database update errors and log them
                LogExceptionToDatabase(ex);
                MessageBox.Show("Error updating status ! " + ex.Message);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
