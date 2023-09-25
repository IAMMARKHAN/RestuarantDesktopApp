using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using db2021finalprojectg_9;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;

namespace Restuarant_App
{
    public partial class Login : Form
    {
        public  string loginTime;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
                this.Hide();
                SignUp S = new SignUp();
                S.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForgotPassword S = new ForgotPassword();
            S.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                string email = textBox2.Text;
                string password = textBox3.Text;
                try
                {
                    string query = "SELECT COUNT(*) FROM dbo.[user] WHERE email = @Email AND password = @Password";
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@Email", email); // replace with the name of your username input textbox
                    command.Parameters.AddWithValue("@Password", password); // replace with the name of your password input textbox
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        var con2 = Configuration.getInstance().getConnection();
                        string query2 = "SELECT name, role FROM dbo.[user] WHERE email = @Email";
                        SqlCommand command2 = new SqlCommand(query2, con2);
                        command2.Parameters.AddWithValue("@Email", email);

                        using (var reader = command2.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string userName = reader["name"].ToString().Trim();
                                string userRole = reader["role"].ToString().Trim();

                                if (userRole == "admin")
                                {
                                    this.Hide();
                                    DateTime now = DateTime.Now;
                                    loginTime = now.ToString("hh:mm:ss tt");

                                    AdminMain A = new AdminMain(loginTime, userName);
                                    A.Show();
                                }
                                else
                                {
                                    this.Hide();
                                    CustomerMain A = new CustomerMain();
                                    A.Show();
                                }
                            }
                        }

                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error Occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Enter All Fields!");
            }
        }
    }
}
