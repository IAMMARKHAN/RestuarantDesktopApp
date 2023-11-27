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
using Stripe;


namespace Restuarant_App
{
    public partial class Payment : Form
    {
        decimal amount;
        string name, address,ammount,list;
        int quantity;
        CCart C;
        public Payment(string name,string address, int quantity ,string amount,string list, CCart C)
        {
            InitializeComponent();
            this.name = name;
            this.quantity = quantity;
            this.ammount= amount;
            this.list= list;
            this.address = address;
            this.C= C;
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
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                if (textBox1.Text.Length == 16)
                {
                    if (textBox3.Text.Length == 3)
                    {
                        string input = textBox4.Text.Trim();
                        string pattern = @"^(0[1-9]|1[0-2])\/\d{2}$";

                        if (!Regex.IsMatch(input, pattern))
                        {
                            MessageBox.Show("Invalid Date! Enter In MM/YY Format!");
                        }
                        else
                        {
                            StripeConfiguration.ApiKey = "sk_test_51OFCGyBv8EqRukFuQIL3mp6kR9uVw0xSGBz7Ntp9H2VTc0WP7pj3mUbwmngKLy4H7R3tRP1x2F2LXDhTZIW6STy500FvRDL4V4";

                            var options = new TokenCreateOptions
                            {
                                Card = new TokenCardOptions
                                {
                                    Number = "tok_visa",
                                    ExpMonth = "09",
                                    ExpYear = "2028"
                                }
                            };

                            var service = new TokenService();

                            var chargeOptions = new ChargeCreateOptions
                            {
                                Amount = 1000,
                                Currency = "usd",
                                Description = "Payment For Food",
                                Source = "tok_visa"
                            };

                            var chargeService = new ChargeService();
                            Charge charge = chargeService.Create(chargeOptions);

                            if (charge.Paid)
                            {
                                MessageBox.Show("Test Payment Successful!");
                                this.Hide();
                                CCart C = new CCart(name,address);
                                C.PaymentSuccessfull(quantity,ammount, list,C);
                            }
                            else
                            {
                                MessageBox.Show("Test Payment Failed !");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter Valid 3-Digit CVC Number!");
                    }
                }
                else
                {
                    MessageBox.Show("Enter Valid 16-Digit Card Number!");
                }
            }
            else
            {
                MessageBox.Show("Enter All Values!");
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
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
