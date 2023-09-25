using db2021finalprojectg_9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_App
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login S = new Login();
            S.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //From Textboxes Into Variables
            string name = textBox2.Text.Trim();
            string email = textBox1.Text.Trim();
            string password = textBox4.Text;
            string address = textBox5.Text.Trim();
            string contact = textBox3.Text.Trim();

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
            if (check==-1)
            {
                MessageBox.Show("Email Already Used!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (check>0)
            {
                MessageBox.Show("Account Created Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Login LS= new Login();
                LS.Show();
            }
            else
            {
                MessageBox.Show("Error Creating Account!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string insertQuery = "INSERT INTO dbo.[user] (name, email, password, address, contact, role) VALUES (@Name, @Email, @Password, @Address, @Contact, @Role)";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(insertQuery, con);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Contact", contact);
                command.Parameters.AddWithValue("@Role", "customer");
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected ;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        private bool IsValidEmail(string email)
        {
            // Use a regular expression to validate the email address
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
                return count > 0; // Return true if the email exists, false otherwise
            }
            catch (Exception ex)
            {
                // Handle exceptions here, e.g., log the error
                return false;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
                
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                // Check if the entered character is not an alphabet
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true; // Block the character if it's not an alphabet
                }
            }
        }
    }
}
