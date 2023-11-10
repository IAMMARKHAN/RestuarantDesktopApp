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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_App
{
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true; 
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            

        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private int InsertUserData(string name, string email, string password, string address, string contact)
        {

            try
            {
                if (EmailExistsInDatabase(email))
                {
                    return -1;
                }
                string insertQuery = "INSERT INTO dbo.[user] (name, email, password, address, contact, role,active,createdAt,updatedAt) VALUES (@Name, @Email, @Password, @Address, @Contact, @Role,@f,@g,@h)";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(insertQuery, con);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Contact", contact);
                command.Parameters.AddWithValue("@Role", "admin");
                command.Parameters.AddWithValue("@f", true);
                command.Parameters.AddWithValue("@g", DateTime.Now);
                command.Parameters.AddWithValue("@h", DateTime.Now);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
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


        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        private bool EmailExistsInDatabase(string email)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM dbo.[user] WHERE email = @Email";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Email", email);
                int count = (int)command.ExecuteScalar();
                return count > 0; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string name = textBox2.Text.Trim();
            string email = textBox1.Text.Trim();
            string password = textBox3.Text;
            string address = textBox4.Text.Trim();
            string contact = textBox5.Text.Trim();

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(contact) ||
                !IsValidEmail(email))
            {
                MessageBox.Show("Please Fill All Fields With Valid Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int check = InsertUserData(name, email, password, address, contact);
            if (check == -1)
            {
                MessageBox.Show("Email Already Used!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (check > 0)
            {
                MessageBox.Show("Account Created Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();

            }
            else
            {
                MessageBox.Show("Error Creating Account!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
