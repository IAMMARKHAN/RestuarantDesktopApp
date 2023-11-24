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

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT Id, Name, CategoryId, Price, Size, Active FROM menu Where Active=@A";
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
                string query = "SELECT Id,Name,CategoryId,Price,Size, Active FROM menu Where CategoryId=@a And Active=@V";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@a", name);
                command.Parameters.AddWithValue("@V", true);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
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
            string buttonText = "Spanish";
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

        private void CMenu_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT Id, Name, CategoryId, Price, Size, Active FROM menu Where Active=@A";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@A", true);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
