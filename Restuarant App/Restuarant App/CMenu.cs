using db2021finalprojectg_9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_App
{
    public partial class CMenu : Form
    {
        public int Price;
        public string Name, Desciption;
        public CMenu()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name.Equals("Image") && e.RowIndex >= 0)
            {
                byte[] imageBytes = (byte[])dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                Image originalImage = ByteArrayToImage(imageBytes);
                int maxWidth = 50; 
                int maxHeight = 50; 

                Image resizedImage = ResizeImage(originalImage, maxWidth-18, maxHeight-10);
                e.Value = resizedImage;
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }


        private Image ResizeImage(Image originalImage, int maxWidth, int maxHeight)
        {
            int newWidth, newHeight;
            if (originalImage.Width > originalImage.Height)
            {
                newWidth = maxWidth;
                newHeight = (int)((float)originalImage.Height / originalImage.Width * newWidth);
            }
            else
            {
                newHeight = maxHeight;
                newWidth = (int)((float)originalImage.Width / originalImage.Height * newHeight);
            }

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            return resizedImage;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT Id, Name, Price, Size, Image FROM menu Where Active=@A";
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@A", true);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
        }
        public void PopulateDataGridView1(int name)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT Id,Name,Price,Size,Image FROM menu Where CategoryId=@a And Active=@V";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@a", name);
                command.Parameters.AddWithValue("@V", true);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                dataGridView2.CellFormatting += dataGridView2_CellFormatting;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonText = "Desi";
            var con = Configuration.getInstance().getConnection();
            string query = "SELECT Id FROM categories WHERE Name = @categoryName";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@categoryName", buttonText);
            command.Parameters.AddWithValue("@B", true);
            object result = command.ExecuteScalar(); 
            if (result != null)
            {
                int categoryId = Convert.ToInt32(result); 
                PopulateDataGridView1(categoryId);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonText = "Italian";
            var con = Configuration.getInstance().getConnection();
            string query = "SELECT Id FROM categories WHERE Name = @categoryName";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@categoryName", buttonText);
            command.Parameters.AddWithValue("@B", true);
            object result = command.ExecuteScalar();
            if (result != null)
            {
                int categoryId = Convert.ToInt32(result); 
                PopulateDataGridView1(categoryId);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonText = "French";
            var con = Configuration.getInstance().getConnection();
            string query = "SELECT Id FROM categories WHERE Name = @categoryName";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@categoryName", buttonText);
            command.Parameters.AddWithValue("@B", true);
            object result = command.ExecuteScalar();
            if (result != null)
            {
                int categoryId = Convert.ToInt32(result);
                PopulateDataGridView1(categoryId);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonText = "Continental";
            var con = Configuration.getInstance().getConnection();
            string query = "SELECT Id FROM categories WHERE Name = @categoryName";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@categoryName", buttonText);
            command.Parameters.AddWithValue("@B", true);
            object result = command.ExecuteScalar(); 
            if (result != null)
            {
                int categoryId = Convert.ToInt32(result); 
                PopulateDataGridView1(categoryId);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].Name == "Add")
            {
                DialogResult result = MessageBox.Show("Add This Item To Cart ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                    string idColumnName = "Id";
                    string nameColumnName = "Name";
                    string priceColumnName = "Price";
                    string sizeColumnName = "Size";


                    int idValue = Convert.ToInt32(row.Cells[idColumnName].Value);
                    string nameValue = row.Cells[nameColumnName].Value.ToString();
                    int priceValue = Convert.ToInt32(row.Cells[priceColumnName].Value);
                    string sizeValue = row.Cells[sizeColumnName].Value.ToString();
                    CCart.cartItems.Add(new CartItem
                    {
                        Id = idValue,
                        Name = nameValue,
                        Price = priceValue,
                        Size = sizeValue
                    });
                    MessageBox.Show("Added Successfully !", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }

            }
            if (e.ColumnIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].Name == "Favourite")
            {
                DialogResult result = MessageBox.Show("Add This To Favourites ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                    string idColumnName = "Id";
                    string nameColumnName = "Name";
                    string priceColumnName = "Price";
                    string sizeColumnName = "Size";
                    int idValue = Convert.ToInt32(row.Cells[idColumnName].Value);
                    string nameValue = row.Cells[nameColumnName].Value.ToString();
                    int priceValue = Convert.ToInt32(row.Cells[priceColumnName].Value);
                    string sizeValue = row.Cells[sizeColumnName].Value.ToString();
                    CFavourites.cartItems.Add(new CartItem
                    {
                        Id = idValue,
                        Name = nameValue,
                        Price = priceValue,
                        Size = sizeValue
                    });
                    MessageBox.Show("Added Successfully !", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }

            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonText1 = "Cold Beverage";

            var con = Configuration.getInstance().getConnection();
            string query = "SELECT Id FROM categories WHERE Name = @categoryName1 ";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@categoryName1", buttonText1);

            command.Parameters.AddWithValue("@B", true);
            object result = command.ExecuteScalar();
            if (result != null)
            {
                int categoryId = Convert.ToInt32(result);
                PopulateDataGridView1(categoryId);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonText = "Desert";
            var con = Configuration.getInstance().getConnection();
            string query = "SELECT Id FROM categories WHERE Name = @categoryName";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@categoryName", buttonText);
            command.Parameters.AddWithValue("@B", true);
            object result = command.ExecuteScalar();
            if (result != null)
            {
                int categoryId = Convert.ToInt32(result);
                PopulateDataGridView1(categoryId);
            }
        }

        private void CMenu_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT Id, Name, Price, Size, Image FROM menu Where Active=@A";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@A", true);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                dataGridView2.CellFormatting += dataGridView2_CellFormatting;



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
