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
using Restuarant_App._BL;

namespace Restuarant_App
{
    public partial class EditMenu : Form
    {
        string name, size;
        int category;
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
                comboBox1.Items.Clear();

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

            openFileDialog.InitialDirectory = @"C:\Users\YourUsername\Desktop";

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    imgData = File.ReadAllBytes(filePath);
                    textBox3.Text = filePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
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
            try
            {

            var con = Configuration.getInstance().getConnection();

                SqlCommand findCategoryIdCommand = new SqlCommand("SELECT Id FROM categories WHERE Name = @Type", con);
            findCategoryIdCommand.Parameters.AddWithValue("@Type", type);
            int categoryId = (int)findCategoryIdCommand.ExecuteScalar();
             MenuBL M = new MenuBL(name, categoryId, price, size, img, true, DateTime.Now, DateTime.Now);

            SqlCommand command = new SqlCommand("UPDATE menu SET Name = @NewName, CategoryId = @CategoryId, Price = @Price, Size = @Size, Image = @Image, Active = @Active, UpdatedAt = @UpdatedAt WHERE Id = @Id", con);
            command.Parameters.AddWithValue("@NewName", M.Name);
            command.Parameters.AddWithValue("@CategoryId", M.CategoryId); 
            command.Parameters.AddWithValue("@Price", M.Price);
            command.Parameters.AddWithValue("@Size", M.Size);
            command.Parameters.AddWithValue("@Image", M.ImageData);
            command.Parameters.AddWithValue("@Active", M.Active); 
            command.Parameters.AddWithValue("@UpdatedAt", M.UpdatedAt);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return 1;
            }
            return 0;
            }
            catch(Exception E)
            {
                LogExceptionToDatabase(E);
                MessageBox.Show(E.Message);
                return 0;
            }

        }

        int price, id;
        public EditMenu(string name,int category,int id,int price,string size)
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
