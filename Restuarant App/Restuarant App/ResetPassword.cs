using db2021finalprojectg_9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_App
{
    public partial class ResetPassword : Form
    {
        string email;
        public ResetPassword(string email)
        {
            InitializeComponent();
            this.email = email;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            int code=2;
            RecoverPassword recoverPassword = new RecoverPassword(code,email);
            recoverPassword.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox2.Text !="")
            {
                string updateCommandText = "UPDATE dbo.[user] SET password = @NewPassword WHERE email = @Email";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(updateCommandText, con);
                command.Parameters.AddWithValue("@Email", email); 
                command.Parameters.AddWithValue("@NewPassword", textBox2.Text); 
                int count = command.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("Password Updated Successfullly!", "Success", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Please Enter New Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
