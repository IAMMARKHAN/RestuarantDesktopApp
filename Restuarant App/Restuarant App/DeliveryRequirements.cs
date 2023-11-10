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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restuarant_App
{
    public partial class DeliveryRequirements : Form
    {
        public DeliveryRequirements()
        {
            InitializeComponent();
        }

        private void DeliveryRequirements_Load(object sender, EventArgs e)
        {
            try
            {

            string query2 = "SELECT Name FROM dbo.[staff] Where Type=@c";
            var con2 = Configuration.getInstance().getConnection();
            SqlCommand command2 = new SqlCommand(query2, con2);
            command2.Parameters.AddWithValue("@c", "Delivery");

            using (SqlDataReader reader2 = command2.ExecuteReader())
            {
                comboBox1.Items.Clear();

                while (reader2.Read())
                {
                    comboBox1.Items.Add(reader2["Name"].ToString());
                }
            }
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show("Error Loading");
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
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ( comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select All Fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Pos P = new Pos();
                P.delivery=comboBox1.SelectedItem.ToString();
                P.orderType = "Delivery";
                P.waiter = "";
                P.tableId = 0;
                this.Hide();

            }
        }
    }
}
