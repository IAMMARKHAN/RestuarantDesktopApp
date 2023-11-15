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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
                e.Handled = true; 
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
            string d=textBox1.Text.ToString();
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
        private int UpdateUserData(string name,string type,string cont)
        {
            StaffBL S = new StaffBL(name, type, cont, true, DateTime.Now, DateTime.Now);

            try
            {

            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand("UPDATE staff SET Name = @NewName, Type = @NewType,Contact=@Con,UpdatedAt=@CC,Active=@ff WHERE Id = @Id", con);
            command.Parameters.AddWithValue("@NewName", S.Name);
            command.Parameters.AddWithValue("@NewType", S.Type);
            command.Parameters.AddWithValue("@Con", S.Contact);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@CC", S.UpdatedAt);
            command.Parameters.AddWithValue("@ff", S.Active);

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

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
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
