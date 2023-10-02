using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using db2021finalprojectg_9;
using System.Diagnostics;

using System.Windows.Forms;
using iTextSharp.text;

namespace Restuarant_App
{
    public partial class Pos : Form
    {
        public string orderType="";
        public string waiter = "";
        public string delivery = "";
        public int table;



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
                string query = "SELECT Id, Name, CategoryId, Price, Size, Active FROM menu";
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
        

        public void PopulateDataGridView1(int name)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT Id,Name,CategoryId,Price,Size, Active FROM menu Where CategoryId=@a";
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

        public string FindMethod1(int id)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT Name FROM dbo.[categories] WHERE Id=@a";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@a", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    reader.Close();
                    return name;
                }
                
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return "";
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
            Button clickedButton = (Button)sender;
            string buttonText = clickedButton.Text;
        
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT Id FROM categories WHERE Name = @categoryName";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@categoryName", buttonText);
                object result = command.ExecuteScalar(); // Retrieve a single value (the ID)
                if (result != null)
                {
                    int categoryId = Convert.ToInt32(result); // Convert the result to an integer
                                                              // You can use the categoryId for further processing
                    PopulateDataGridView1(categoryId);
                }
              
            

           
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
                int category = Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells["CategoryId"].Value);
                DialogResult result = MessageBox.Show("Add This Item ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    var itemData = FindMethod(idToDelete);
                    var cat = FindMethod1(category);

                    if (itemData.Name != null)
                    {
                        string itemName = itemData.Name;
                        int itemPrice = itemData.Price;
                        dataGridView1.Rows.Add(itemName,cat,'1' ,itemPrice); // Assuming 1 as quantity
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
        public void Populate(string name)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT Id,Name,CategoryId,Price,Size FROM menu Where CategoryId=@a";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@a", name);
                //sql
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

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.White;
            button2.ForeColor = Color.FromArgb(50, 55, 130);

            // Reset the appearance of the other buttons
            // Reset the appearance of the other buttons
            button1.BackColor = Color.FromArgb(50, 55, 130); ;
            button1.ForeColor = Color.White;
            button3.BackColor = Color.FromArgb(50, 55, 130); ;
            button3.ForeColor = Color.White;
            DineInRequirements D = new DineInRequirements();
            D.ShowDialog();
            if(table!=0 && waiter!="")
            {

            orderType = "Dine In";
            }    

        }

        private void button1_Click(object sender, EventArgs e)
        {

            button1.BackColor = Color.White;
            button1.ForeColor = Color.FromArgb(50, 55, 130);

            // Reset the appearance of the other buttons
            // Reset the appearance of the other buttons
            button2.BackColor = Color.FromArgb(50, 55, 130); ;
            button2.ForeColor = Color.White;
            button3.BackColor = Color.FromArgb(50, 55, 130); ;
            button3.ForeColor = Color.White;
            DeliveryRequirements D = new DeliveryRequirements();
            D.ShowDialog();
            if(delivery!="")
            {
            orderType = "Delivery";
            }
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
        private void button5_Click(object sender, EventArgs e)
        {
            bool c1=false, c2=false, c3=false;
            if (orderType == "")
            {
                MessageBox.Show("Please select the order type !");
            }
            else
            {
                c1= true;
            }
            if (textBox1.Text == "" || textBox1.Text == "Enter Customer Name")
            {
                MessageBox.Show("Enter valid customer name  !");
            }
            else
            {
                c2=true;
            }
            if (dataGridView1.Rows.Count==0)
            {
                MessageBox.Show("Add any item first !");
            }
            else
            {
                c3 = true;
            }
            if (c1 == true && c2 == true && c3 == true)
            {
                DialogResult result = MessageBox.Show("Printing Bill ! Is Is Paid ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);



                // Check if the user clicked "Yes"
                if (result == DialogResult.Yes)
                {
                    string savePath;

                    // Show the Save File Dialog to get the file path
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF Files|*.pdf";
                        try
                        {
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                savePath = saveFileDialog.FileName;

                                // Get the desktop directory path
                                Document document = new Document();

                                // Create a PDF document
                                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                                document.Open();

                                // Add header information to the PDF
                                Paragraph title = new Paragraph("Restaurant Management System");
                                title.Alignment = Element.ALIGN_CENTER;
                                document.Add(title);
                                document.Add(new Paragraph("\n"));
                                document.Add(new Paragraph($"Date: {DateTime.Today.ToShortDateString()}"));
                                document.Add(new Paragraph($"Time: {DateTime.Now.ToShortTimeString()}"));
                                document.Add(new Paragraph("Report Type: Bill"));
                                document.Add(new Paragraph("Printed By: Manager"));

                                document.Add(new Paragraph("\n"));

                                // Create a table for the DataGridView content
                                PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    table.AddCell(new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText))
                                    {
                                        HorizontalAlignment = Element.ALIGN_CENTER
                                    });
                                }

                                // Add DataGridView content to the table
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                    {
                                        table.AddCell(new PdfPCell(new Phrase(row.Cells[i].Value.ToString()))
                                        {
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        });
                                    }
                                }

                                // Add the table to the PDF document
                                document.Add(table);
                                // Add total, tax, and net total information to the PDF
                                document.Add(new Paragraph("\n"));
                                Paragraph P = new Paragraph($"Total: {label5.Text}");
                                P.Alignment = Element.ALIGN_RIGHT;
                                document.Add(P);
                                Paragraph P1 = new Paragraph("Tax: 16%");
                                P1.Alignment = Element.ALIGN_RIGHT;
                                document.Add(P1);
                                Paragraph P2 = new Paragraph($"Net Total: {label7.Text}");
                                P2.Alignment = Element.ALIGN_RIGHT;
                                document.Add(P2);
                                document.Close();

                                MessageBox.Show("Bill saved successfully !");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
