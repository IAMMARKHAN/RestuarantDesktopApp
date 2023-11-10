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
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using Restuarant_App._BL;

namespace Restuarant_App
{
    public partial class NewMenu : Form
    {
        public byte[] imageData;
        public NewMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }
        private int InsertUserData(string name, string type, int price,string size,byte[] img)
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand findCategoryIdCommand = new SqlCommand("SELECT Id FROM categories WHERE Name = @Type", con);
            findCategoryIdCommand.Parameters.AddWithValue("@Type", type);
            int categoryId = (int)findCategoryIdCommand.ExecuteScalar();
            MenuBL M = new MenuBL(name,categoryId,price,size,img,true,DateTime.Now,DateTime.Now);
            try
            {
                string insertQuery = "INSERT INTO dbo.[menu] (Name, CategoryId, Price, Size, Image, Active, CreatedAt, UpdatedAt) VALUES (@Name, @CategoryId, @Con, @A, @B, @Ac, @Cr, @Ur)";
                SqlCommand command = new SqlCommand(insertQuery, con);
                command.Parameters.AddWithValue("@Name", M.Name);
                command.Parameters.AddWithValue("@CategoryId", M.CategoryId); // Set CategoryId here
                command.Parameters.AddWithValue("@Con", M.Price);
                command.Parameters.AddWithValue("@A", M.Size);
                command.Parameters.AddWithValue("@B", M.ImageData);
                command.Parameters.AddWithValue("@Ac", M.Active);
                command.Parameters.AddWithValue("@Cr", M.CategoryId);
                command.Parameters.AddWithValue("@Ur", M.UpdatedAt);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {


           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void NewMenu_Load(object sender, EventArgs e)
        {

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
        private void LogExceptionToDatabase(Exception ex)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand("INSERT INTO ErrorLog (ErrorMessage, StackTrace, FunctionName, FileName, LogTime) VALUES (@ErrorMessage, @StackTrace, @FunctionName, @FileName, @LogTime)", con);
            command.Parameters.AddWithValue("@ErrorMessage", ex.Message);
            command.Parameters.AddWithValue("@StackTrace", ex.StackTrace);
            command.Parameters.AddWithValue("@FunctionName", GetCallingMethodName()); // Get calling method name
            command.Parameters.AddWithValue("@FileName", GetFileName()); // Get file name
            command.Parameters.AddWithValue("@LogTime", DateTime.Now);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception logEx)
            {
                // Handle any exceptions that may occur during the logging operation (optional)
                Console.WriteLine("Error while logging exception: " + logEx.Message);
            }
        }

        // Helper function to extract calling method name from stack trace
        private string GetCallingMethodName()
        {
            var frames = new StackTrace(true).GetFrames();
            if (frames != null && frames.Length >= 3)
            {
                // Index 3 represents the calling method in the stack trace
                return frames[3].GetMethod().Name;
            }
            return "Unknown";
        }

        // Helper function to extract file name from stack trace
        private string GetFileName()
        {
            var frames = new StackTrace(true).GetFrames();
            if (frames != null && frames.Length >= 3)
            {
                // Index 3 represents the calling method in the stack trace
                var fileName = frames[3].GetFileName();
                if (fileName != null)
                {
                    return System.IO.Path.GetFileName(fileName);
                }
            }
            return "Unknown";
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedIndex == -1 || textBox1.Text == "" || textBox3.Text == "" || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please Enter All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string name = textBox2.Text.Trim();
            string type = comboBox1.Text.ToString();
            int price = int.Parse(textBox1.Text);
            string size = comboBox2.Text.ToString();
            int check = InsertUserData(name, type, price, size,imageData);
            if (check > 0)
            {
                MessageBox.Show("Added Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error Adding Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Users\YourUsername\Desktop";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Read the selected image file into a byte array
                    imageData = File.ReadAllBytes(filePath);

                    // Store the byte array or use it as needed
                    // For example, you can save it to a database, display it, etc.

                    // Display the file path in textbox3.Text (optional)
                    textBox3.Text = filePath;

                    // You now have the image data in the 'imageData' byte array
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during file reading
                    LogExceptionToDatabase(ex);
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
