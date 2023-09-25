using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restuarant_App
{
    public partial class RecoverPassword : Form
    {
        int code;
        string email;
        public RecoverPassword(int code,string email)
        {
            InitializeComponent();
            this.code = code;
            this.email = email;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForgotPassword S = new ForgotPassword();
            S.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Code Logic
            int entered = int.Parse(textBox2.Text);
            if(entered==code)
            {
            this.Hide();
            ResetPassword S = new ResetPassword(this.email);
            S.Show();
            }
            else
            {
                MessageBox.Show("Code Verification Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters (e.g., backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            // Ensure that the total length is not more than 4 characters
            if (textBox2.Text.Length >= 4 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
