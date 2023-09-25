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
            command.Parameters.AddWithValue("@Email", email); // replace with the name of your username input textbox
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
                    MessageBox.Show($"Error Sending Email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("No Account Found With This Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            
    
        }
        private void SendEmail(string to, string code)
        {
            string smtpServer = "smtp.gmail.com"; // Replace with your SMTP server
            string smtpUsername = "ammariftikhar666@gmail.com";   // Replace with your SMTP username
            string smtpPassword = "mflyhbbpugjtypad\r\n";   // Replace with your SMTP password
            int smtpPort = 587; // Replace with your SMTP port (e.g., 587 for TLS)
            using (SmtpClient client = new SmtpClient(smtpServer))
            {
                client.Port = smtpPort;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;
                using (MailMessage message = new MailMessage(smtpUsername, to))
                {
                    message.Subject = "Verification Code";
                    message.Body = $"Your Verification Code Is: {code}";
                    client.Send(message);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
