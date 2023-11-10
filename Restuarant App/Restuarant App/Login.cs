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
using System.Diagnostics;

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
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password); 
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
                                    string loginTime = now.ToString("hh:mm:ss tt");
                                    AdminMain A = new AdminMain(loginTime, userName);
                                    A.Show();
                                }
                                else
                                {
                                    reader.Close();
                                    string query3 = "SELECT id,address FROM dbo.[user] WHERE email = @Email ";
                                    var con3 = Configuration.getInstance().getConnection();
                                    SqlCommand command3 = new SqlCommand(query3, con3);
                                    command3.Parameters.AddWithValue("@Email", email); 
                                    int count3 = (int)command3.ExecuteScalar();
                                    string userAddress = string.Empty;
                                    using (SqlDataReader reader5 = command3.ExecuteReader())
                                    {
                                        if (reader5.Read())
                                        {
                                            userAddress = reader5["Address"].ToString();
                                        }
                                    }
                                    DateTime now = DateTime.Now;
                                    string loginTime = now.ToString("hh:mm:ss tt");
                                    this.Hide();
                                    CustomerMain A = new CustomerMain(loginTime, userName, count3,userAddress);
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
                    LogExceptionToDatabase(ex);
                    MessageBox.Show("An Error Occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter All Fields!");
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
    }
}
