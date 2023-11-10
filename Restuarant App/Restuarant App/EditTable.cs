using db2021finalprojectg_9;
using Restuarant_App._BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Restuarant_App
{
    public partial class EditTable : Form
    {
        public int id, seats;
        public string type;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EditTable_Load(object sender, EventArgs e)
        {
            textBox2.Text = seats.ToString();
            comboBox1.Text = type;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text.ToString()) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Enter All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int check = UpdateUserData(int.Parse(textBox2.Text), comboBox1.Text.ToString());
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
        private int UpdateUserData(int seats, string type)
        {
            TablesBL T = new TablesBL(seats, type, true, DateTime.Now, DateTime.Now);

            try
            {


                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand("UPDATE tables SET Seats = @NewName, Located = @NewType, UpdatedAt=@g,Active=@gd WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@NewName", T.Seats);
                command.Parameters.AddWithValue("@NewType", T.Located);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@g", T.UpdatedAt);
                command.Parameters.AddWithValue("@gd", T.Active);


                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return 1;
                }
                return 0;
            }
            catch(Exception ex) { 
            LogExceptionToDatabase(ex);
                MessageBox.Show(ex.Message);
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        public EditTable(int seats,string type,int id)
        {
            InitializeComponent();
            this.seats = seats;
            this.type = type;
            this.id = id;
        }
    }
}
