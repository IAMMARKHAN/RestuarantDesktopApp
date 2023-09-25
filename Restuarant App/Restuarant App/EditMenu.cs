using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows.Forms;
using System.Xml.Linq;
using db2021finalprojectg_9;
using System.Data.SqlClient;

namespace Restuarant_App
{
    public partial class EditMenu : Form
    {
        string name, category, size;
        public byte[] imgData;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EditMenu_Load(object sender, EventArgs e)
        {
            textBox1.Text = price.ToString();
            textBox2.Text = name;

            string query = "SELECT Name FROM categories";
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(query, con);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Clear existing items in the ComboBox
                comboBox1.Items.Clear();

                // Loop through the result set and add items to the ComboBox
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["Name"].ToString());
                }
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the initial directory (optional)
            openFileDialog.InitialDirectory = @"C:\Users\YourUsername\Desktop";

            // Filter to only show image files
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Read the selected image file into a byte array
                    imgData = File.ReadAllBytes(filePath);

                    // Store the byte array or use it as needed
                    // For example, you can save it to a database, display it, etc.

                    // Display the file path in textbox3.Text (optional)
                    textBox3.Text = filePath;

                    // You now have the image data in the 'imageData' byte array
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during file reading
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Block the input
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text.ToString()) || string.IsNullOrEmpty(textBox3.Text.ToString()) || comboBox2.SelectedIndex == -1 || comboBox1.SelectedIndex == -1 || textBox1.Text == "")
            {
                MessageBox.Show("Please Enter All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int check = UpdateUserData(textBox2.Text, comboBox1.Text.ToString(),int.Parse(textBox1.Text),comboBox2.Text.ToString(),imgData);
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
        private int UpdateUserData(string name, string type,int price,string size, byte[] img)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand("UPDATE menu SET Name = @NewName, Category = @NewType,Price=@Con ,Size=@Con1 ,Image=@Con2 WHERE Id = @Id", con);
            command.Parameters.AddWithValue("@NewName", name);
            command.Parameters.AddWithValue("@NewType", type);
            command.Parameters.AddWithValue("@Con", price);
            command.Parameters.AddWithValue("@Con1", size);
            command.Parameters.AddWithValue("@Con2", img);
            command.Parameters.AddWithValue("@Id", id);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return 1;
            }
            return 0;

        }

        int price, id;
        public EditMenu(string name,string category,int id,int price,string size)
        {
            InitializeComponent();
            this.name = name;
            this.category = category;
            this.id = id;
            this.price = price;
            this.size = size;
        }
    }
}
