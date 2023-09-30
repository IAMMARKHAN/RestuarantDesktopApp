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
    public partial class NewTable : Form
    {
        public NewTable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       

        private void button5_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(textBox2.Text.ToString()) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Enter All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int seats = int.Parse(textBox2.Text);
            string type = comboBox1.Text.ToString();
            int check = InsertUserData(seats, type);
            if (check > 0)
            {
                MessageBox.Show("Added Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error Adding Data !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int InsertUserData(int seats, string email)
        {

            try
            {

                string insertQuery = "INSERT INTO dbo.[tables] (Seats,Located) VALUES (@Name, @Email)";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(insertQuery, con);
                command.Parameters.AddWithValue("@Name", seats);
                command.Parameters.AddWithValue("@Email", email);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                return 0;
            }


        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
