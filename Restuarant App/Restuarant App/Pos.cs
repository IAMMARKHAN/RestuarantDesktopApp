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
    public partial class Pos : Form
    {
        public string orderType="";
        public Pos()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Pos_Load(object sender, EventArgs e)
        {
            LoadButtonsFromDatabase();
            PopulateDataGridView();


        }

        public void PopulateDataGridView()
        {
            // Replace with your connection string

            try
            {
                string query = "SELECT Id,Name,Category,Price,Size FROM menu";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
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

        public void PopulateDataGridView1(string name)
        {
            // Replace with your connection string

            try
            {
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT Id,Name,Category,Price,Size FROM menu Where Category=@a";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@a", name);
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
        public (string Name, int Price) FindMethod(int id)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT Name, Price FROM menu WHERE Id=@a";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@a", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    int price = Convert.ToInt32(reader["Price"]);
                    reader.Close();
                    return (name, price);
                }
                else
                {
                    // Handle the case where the record with the specified ID was not found.
                    return (null, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return (null, 0); // Return null values to indicate an error occurred
            }
        }


        private void LoadButtonsFromDatabase()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();

                string query = "SELECT Name FROM dbo.[categories]"; // Remove "TOP 1" to fetch all records
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) // Loop through all records
                {
                    string categoryName = reader["Name"].ToString();
                    CreateButton(categoryName);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading buttons from the database: " + ex.Message);
            }
        }

        private void CreateButton(string categoryName)
        {
            Button button = new Button
            {
                Text = categoryName,
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                ForeColor= Color.White,
                BackColor = System.Drawing.Color.FromArgb(50, 55, 140)
            };
            button.FlatAppearance.BorderSize = 0;
            // Add an event handler for the button click event, if needed
            // button.Click += YourButtonClickHandler;
            button.Click += Button_Click;


            // Add the button to TableLayoutPanel1
            tableLayoutPanel1.Controls.Add(button);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            // Cast the sender object back to a Button to get the clicked button
            Button clickedButton = (Button)sender;

            // Now you can work with the clickedButton, for example:
            string buttonText = clickedButton.Text;
            PopulateDataGridView1(buttonText);
           
        }
        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PopulateDataGridView();


        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].Name == "Add")
            {
                int rowIndex = e.RowIndex; // Get the clicked row index
                int idToDelete = Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells["Id"].Value);

                // Ask for confirmation
                DialogResult result = MessageBox.Show("Add This Item ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    // Assuming you have a method to retrieve data from the menu table based on 'idToDelete'
                    // Replace 'YourDatabaseMethod' with your actual database method
                    var itemData = FindMethod(idToDelete);

                    if (itemData.Name != null)
                    {
                        string itemName = itemData.Name;
                        int itemPrice = itemData.Price;
                        dataGridView1.Rows.Add(itemName,'1' ,itemPrice); // Assuming 1 as quantity
                        label5.Text = (int.Parse(label5.Text) + itemPrice).ToString();
                        decimal tax = (decimal.Parse(label5.Text) ) * 0.16m;
                        label7.Text = (int.Parse(label5.Text) + tax).ToString();

                    }

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            label5.Text = "0";
            label7.Text = "0";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            orderType = "Dine In";
            button2.BackColor = Color.White;
            button2.ForeColor = Color.FromArgb(50, 55, 130);

            // Reset the appearance of the other buttons
            // Reset the appearance of the other buttons
            button1.BackColor = Color.FromArgb(50, 55, 130); ;
            button1.ForeColor = Color.White;
            button3.BackColor = Color.FromArgb(50, 55, 130); ;
            button3.ForeColor = Color.White;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            orderType = "Delivery";
            button1.BackColor = Color.White;
            button1.ForeColor = Color.FromArgb(50, 55, 130);

            // Reset the appearance of the other buttons
            // Reset the appearance of the other buttons
            button2.BackColor = Color.FromArgb(50, 55, 130); ;
            button2.ForeColor = Color.White;
            button3.BackColor = Color.FromArgb(50, 55, 130); ;
            button3.ForeColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            orderType = "Take Away";
            button3.BackColor = Color.White;
            button3.ForeColor = Color.FromArgb(50, 55, 130);

            // Reset the appearance of the other buttons
            button1.BackColor = Color.FromArgb(50, 55, 130); ;
            button1.ForeColor = Color.White;
            button2.BackColor = Color.FromArgb(50, 55, 130); ;
            button2.ForeColor = Color.White;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(orderType=="")
            {
                MessageBox.Show("Please select the order type !");
            }
            if (textBox1.Text == "" || textBox1.Text=="Enter Customer Name")
            {
                MessageBox.Show("Enter name of customer !");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                // Check if the entered character is not an alphabet
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true; // Block the character if it's not an alphabet
                }
            }
        }
    }
}
