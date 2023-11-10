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
using Restuarant_App._BL;

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
            CategoriesBL C = new CategoriesBL(name, email, true, DateTime.Now, DateTime.Now);
            try
            {   
                string insertQuery = "INSERT INTO dbo.[categories] (name,type,active,createdAt,updatedAt) VALUES (@Name, @Email,@A,@B,@C)";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(insertQuery, con);
                command.Parameters.AddWithValue("@Name", C.Name);
                command.Parameters.AddWithValue("@Email",C.Type);
                command.Parameters.AddWithValue("@A", C.Active);
                command.Parameters.AddWithValue("@B", C.CreatedAt);
                command.Parameters.AddWithValue("@C", C.UpdatedAt);
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
            command.Parameters.AddWithValue("@FunctionName", GetCallingMethodName()); 
            command.Parameters.AddWithValue("@FileName", GetFileName()); 
            command.Parameters.AddWithValue("@LogTime", DateTime.Now);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception logEx)
            {
                LogExceptionToDatabase(ex);
                Console.WriteLine("Error while logging exception: " + logEx.Message);
            }
        }

        private string GetCallingMethodName()
        {
            var frames = new StackTrace(true).GetFrames();
            if (frames != null && frames.Length >= 3)
            {
                return frames[3].GetMethod().Name;
            }
            return "Unknown";
        }

        private string GetFileName()
        {
            var frames = new StackTrace(true).GetFrames();
            if (frames != null && frames.Length >= 3)
            {
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
