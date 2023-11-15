using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Restuarant_App
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Payment_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                if(textBox1.Text.Length==16)
                {
                    if (textBox3.Text.Length == 3)
                    {
                        string input = textBox4.Text.Trim();

                        string pattern = @"^(0[1-9]|1[0-2])\/\d{2}$";

                        if (!Regex.IsMatch(input, pattern))
                        {
                            MessageBox.Show("Invalid Date ! Enter In MM/YY Format !");
                        }
                        else
                        {
                            MessageBox.Show("Payment Successfull !");
                            CCart C = new CCart("","");
                            this.Hide();
                            C.PaymentSuccessfull();


                        }

                    }
                    else
                    {
                        MessageBox.Show("Enter Valid 3-Digit Cvc Number !");
                    }

                }
                else
                {
                    MessageBox.Show("Enter Valid 16-Digit Card Number !");
                }

            }
            else
            {
                MessageBox.Show("Enter All Values !");

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numeric digits and control keys (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
            {
                e.Handled = true;
            }
        }
    }
}
