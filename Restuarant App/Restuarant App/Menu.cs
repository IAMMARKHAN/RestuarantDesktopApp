using db2021finalprojectg_9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Restuarant_App
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewMenu M = new NewMenu();
            M.ShowDialog();
            RefreshDataGridView();
        }
        public void PopulateDataGridView()
        {
            dataGridView1.DataSource = null;
            try
            {
                string query = "SELECT * FROM menu";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns["Image"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns["Image"].DefaultCellStyle.NullValue = null; 

                dataGridView1.CellFormatting += (sender, e) =>
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "Image" && e.Value != DBNull.Value)
                    {
                        byte[] imageBytes = (byte[])e.Value;
                        Image image = ConvertBytesToImage(imageBytes);
                        e.Value = ResizeImage(image, dataGridView1.Columns["Image"].Width-18, dataGridView1.Rows[e.RowIndex].Height);
                        e.FormattingApplied = true;
                    }
                };
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private Image ConvertBytesToImage(byte[] imageBytes)
        {
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
            }
        }

        private Image ResizeImage(Image originalImage, int newWidth, int newHeight)
        {
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }
            return newImage;
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "DELETE")
            {
                int rowIndex = e.RowIndex;
                int idToDelete = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to in-active this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand("UPDATE menu SET Active = 0 WHERE Id = @ID", con);
                    command.Parameters.AddWithValue("@ID", idToDelete);
                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            RefreshDataGridView();                        
                        }
                        else
                        {
                            MessageBox.Show("Record Not Found or Not Updated.");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptionToDatabase(ex);
                        MessageBox.Show("Error: " + ex.Message);
                    }


                }
            }

            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "EDIT")
            {
                int rowIndex = e.RowIndex; 
                string name = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["Name"].Value);
                int category = int.Parse(dataGridView1.Rows[rowIndex].Cells["CategoryId"].Value.ToString());
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
                int price = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Price"].Value);
                string size = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["Size"].Value);
                EditMenu E = new EditMenu(name, category, id,price,size);
                E.ShowDialog();
                RefreshDataGridView();
            }
        }
        private void RefreshDataGridView()
        {
            dataGridView1.DataSource = null;
            DataTable updatedDataTable = RetrieveUpdatedData();
            dataGridView1.DataSource = updatedDataTable;
        }
        private DataTable RetrieveUpdatedData()
        {
            string query = "SELECT * FROM menu";
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
