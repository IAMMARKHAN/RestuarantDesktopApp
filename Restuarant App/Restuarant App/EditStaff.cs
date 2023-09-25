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
    public partial class EditStaff : Form
    {
        public string name, type;
        int id;
        double contact;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Block the input
            }
        }

        private void EditStaff_Load(object sender, EventArgs e)
        {
            textBox1.Text = contact.ToString();
            textBox2.Text=name.ToString();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text.ToString()) || comboBox1.SelectedIndex == -1 || textBox1.Text=="")
            {
                MessageBox.Show("Please Enter All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double d=double.Parse(textBox1.Text);
            int check = UpdateUserData(textBox2.Text,comboBox1.Text.ToString(),d);
            if (check > 0)
            {
                MessageBox.Show("Updated Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error Updating Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }
        private int UpdateUserData(string name,string type,double cont)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand("UPDATE staff SET Name = @NewName, Type = @NewType,Contact=@Con WHERE Id = @Id", con);
            command.Parameters.AddWithValue("@NewName", name);
            command.Parameters.AddWithValue("@NewType", type);
            command.Parameters.AddWithValue("@Con", cont);
            command.Parameters.AddWithValue("@Id", id);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return 1;
            }
            return 0;

        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Block the input
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public EditStaff(string name,string type,int id,double contact)
        {
            InitializeComponent();
            this.name = name;
            this.type = type;
            this.id = id;
            this.contact = contact;
            this.contact = contact;
        }
    }
}
