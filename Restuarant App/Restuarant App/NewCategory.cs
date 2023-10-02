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
    public partial class NewCategory : Form
    {
        public NewCategory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text.Trim();
            string type=comboBox1.Text.ToString();

            if (string.IsNullOrEmpty(name) || comboBox1.SelectedIndex==-1)
            {
                MessageBox.Show("Please Enter All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int check = InsertUserData(name,type);
            if (check > 0)
            {
                MessageBox.Show("Added Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();                
            }
            else
            {
                MessageBox.Show("Error Adding Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int InsertUserData(string name, string email)
        {

            try
            {   
                string insertQuery = "INSERT INTO dbo.[categories] (name,type,active,createdAt,updatedAt) VALUES (@Name, @Email,@A,@B,@C)";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(insertQuery, con);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email",email);
                command.Parameters.AddWithValue("@A", "True");
                command.Parameters.AddWithValue("@B", DateTime.Now);
                command.Parameters.AddWithValue("@C", DateTime.Now);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                return 0;
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
                LogExceptionToDatabase(ex);
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
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
