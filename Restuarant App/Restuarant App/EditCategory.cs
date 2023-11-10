using db2021finalprojectg_9;
using Restuarant_App._BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        private int UpdateUserData(string name, string email)
        {
            CategoriesBL C = new CategoriesBL(name, email, true, DateTime.Now, DateTime.Now);
            try
            {

            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand("UPDATE categories SET name = @NewName, type = @NewType,Active=@A,UpdatedAt=@B  WHERE id = @Id", con);
            command.Parameters.AddWithValue("@NewName", C.Name);
            command.Parameters.AddWithValue("@NewType", C.Type);
            command.Parameters.AddWithValue("@A", C.Active);
            command.Parameters.AddWithValue("@B", C.UpdatedAt);

            command.Parameters.AddWithValue("@Id", id);
            
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception Ex){
                LogExceptionToDatabase(Ex);
                MessageBox.Show(Ex.Message);
                return 0;

            }



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide  ();
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }

        }
    }
}
