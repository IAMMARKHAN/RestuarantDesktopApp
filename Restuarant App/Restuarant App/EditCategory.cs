using db2021finalprojectg_9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Restuarant_App
{
    public partial class EditCategory : Form
    {
        public string CategoryName;
        public string type;
        public int id;
        public EditCategory(string name,string type,int id)
        {
            InitializeComponent();
            this.CategoryName = name;
            this.type = type;
            this.id = id;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void EditCategory_Load(object sender, EventArgs e)
        {
            textBox2.Text = CategoryName;
            comboBox1.Text = type;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Enter All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int check = UpdateUserData(textBox2.Text,comboBox1.Text.ToString());
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
        private int UpdateUserData(string name, string email)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand("UPDATE categories SET name = @NewName, type = @NewType WHERE id = @Id", con);
            command.Parameters.AddWithValue("@NewName", name);
            command.Parameters.AddWithValue("@NewType", email);
            command.Parameters.AddWithValue("@Id", id);
            
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return 1;
                }
                return 0;
            
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide  ();
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Block the input
            }

        }
    }
}
