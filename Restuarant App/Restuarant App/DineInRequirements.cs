using db2021finalprojectg_9;
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
using System.Windows.Forms.VisualStyles;

namespace Restuarant_App
{
    public partial class DineInRequirements : Form
    {
        public DineInRequirements()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex==-1 || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

            }
        }

        private void DineInRequirements_Load(object sender, EventArgs e)
        {
            try
            {

                string query = "SELECT Id FROM dbo.[tables]";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    comboBox1.Items.Clear();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["Id"].ToString());
                    }
                }

                string query2 = "SELECT Name FROM dbo.[staff] Where Type=@c";
                var con2 = Configuration.getInstance().getConnection();
                SqlCommand command2 = new SqlCommand(query2, con2);
                command2.Parameters.AddWithValue("@c", "Waiter");
                using (SqlDataReader reader2 = command2.ExecuteReader())
                {
                    comboBox2.Items.Clear();

                    while (reader2.Read())
                    {
                        {
                            comboBox2.Items.Add(reader2["Name"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                LogExceptionToDatabase(ex);
                MessageBox.Show(ex.Message);
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1 || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Pos P = new Pos();
                P.waiter = comboBox2.SelectedItem.ToString();
                P.tableId=int.Parse(comboBox1.SelectedItem.ToString());
                P.orderType = "Dine In";
                P.delivery = "";
                this.Hide();

            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
