using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using db2021finalprojectg_9;
using System.Diagnostics;

namespace Restuarant_App
{
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login S = new Login();
            S.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool EmailExistsInDatabase(string email)
        {
            var con = Configuration.getInstance().getConnection();
            string query = "SELECT COUNT(*) FROM dbo.[user] WHERE email = @Email";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@Email", email); 
            int count = (int)command.ExecuteScalar();
            if(count>0)
            {
                return true;
            }
             return false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Please Enter A Valid Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool check = EmailExistsInDatabase(email);
            if (check==true)
            {
                Random random = new Random();
                int code = random.Next(1000, 10000);
                try
                {
                    SendEmail(email, code.ToString());
                    MessageBox.Show($"A 4-Digit Code Has Been Sent to {email}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    RecoverPassword S = new RecoverPassword(code,email);
                    S.Show();
                }
                catch (Exception ex)
                {
                    LogExceptionToDatabase(ex);
                    MessageBox.Show($"Error Sending Email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("No Account Found With This Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

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
        private void SendEmail(string to, string code)
        {
            try
            {
                string smtpServer = "smtp.gmail.com"; 
                string smtpUsername = "ammariftikhar666@gmail.com";   
                string smtpPassword = "mflyhbbpugjtypad\r\n";   
                int smtpPort = 587; 
                using (SmtpClient client = new SmtpClient(smtpServer))
                {
                    client.Port = smtpPort;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;
                    using (MailMessage message = new MailMessage(smtpUsername, to))
                    {
                        message.Subject = "Verification Code";
                        message.Body = $"Hey! Your 4-digit Verification Code Is: {code}, Enter The Provided Code to Recover Your Account.";
                        client.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
