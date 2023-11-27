﻿using System;
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
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;
using iTextSharp.text.pdf.draw;

namespace Restuarant_App
{
    public partial class Pos : Form
    {
        public string orderType="";
        public string waiter = "";
        public string delivery = "";
        public int tableId=0;
        public int staffId=0;




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

            try
            {
                string query = "SELECT Id, Name, CategoryId, Price, Size FROM menu Where Active=@A";
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
                    return (null, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return (null, 0); 
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

                string query = "SELECT Name FROM dbo.[categories]"; 
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) 
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
            button.Click += Button_Click;
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
            command.Parameters.AddWithValue("@B", true);
            object result = command.ExecuteScalar(); 
                if (result != null)
                {
                    int categoryId = Convert.ToInt32(result); 
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
                int rowIndex = e.RowIndex; 
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
                        dataGridView1.Rows.Add(itemName,cat,'1' ,itemPrice); 
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
            button2.BackColor = Color.FromArgb(50, 55, 130); ;
            button2.ForeColor = Color.White;
            button1.BackColor = Color.FromArgb(50, 55, 130); ;
            button1.ForeColor = Color.White;
            button3.BackColor = Color.FromArgb(50, 55, 130); ;
            button3.ForeColor = Color.White;
            label5.Text = "0";
            label7.Text = "0";
            label12.Text = "0";
            textBox1.Text = "";
            orderType = "";
            txtAddress.Text = "";
            txtName.Text = "";


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
            button1.BackColor = Color.FromArgb(50, 55, 130); ;
            button1.ForeColor = Color.White;
            button3.BackColor = Color.FromArgb(50, 55, 130); ;
            button3.ForeColor = Color.White;
            orderType = "Dine In";

        }

        private void button1_Click(object sender, EventArgs e)
        {

            button1.BackColor = Color.White;
            button1.ForeColor = Color.FromArgb(50, 55, 130);

       
            button2.BackColor = Color.FromArgb(50, 55, 130); ;
            button2.ForeColor = Color.White;
            button3.BackColor = Color.FromArgb(50, 55, 130); ;
            button3.ForeColor = Color.White;
            orderType = "Delivery";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            orderType = "Take Away";
            button3.BackColor = Color.White;
            button3.ForeColor = Color.FromArgb(50, 55, 130);

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
        private void button5_Click(object sender, EventArgs e)
        {
            bool c1 = false, c2 = false, c3 = false, c4 = false;
            if (orderType == "")
            {
                MessageBox.Show("Please Select The Order Type !");
            }
            else
            {
                c1= true;
            }
        
            if (txtName.Text == "" || txtAddress.Text=="")
            {
                MessageBox.Show("Enter Customer Information !");
            }
            else
            {
                c2 = true;
            }
            if (dataGridView1.Rows.Count==0)
            {
                MessageBox.Show("Add Any Item Into Cart First !");
            }
            else
            {
                c3 = true;
            }
            if (textBox1.Text=="")
            {
                MessageBox.Show("Enter Paid Amount !");
            }
            else
            {
                c4 = true;
            }

            if (c1 == true && c2 == true && c3 == true && c4==true)
            {
                DialogResult result = MessageBox.Show("Printing Bill ! Is It Paid ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string savePath;

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF Files|*.pdf";
                        try
                        {
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                savePath = saveFileDialog.FileName;
                                Document document = new Document();
                                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                                document.Open();
                                Paragraph restaurantInfo = new Paragraph();
                                restaurantInfo.Alignment = Element.ALIGN_CENTER;
                                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("D:\\SEMESTER 5\\SE LAB\\Developer\\Project\\Git Project\\Restuarant App\\Restuarant App\\Resources\\Restaurant-logo-with-chef-drawing-template-on-transparent-background-PNG.png");
                                logo.ScalePercent(2f);
                                restaurantInfo.Add(logo);
                                restaurantInfo.Alignment = Element.ALIGN_CENTER;
                                document.Add(restaurantInfo);
                                document.Add(new Paragraph("\n"));
                                LineSeparator line = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1);
                                document.Add(line);
                                Paragraph restaurantName = new Paragraph("Restaurant Management System");
                                restaurantName.Alignment = Element.ALIGN_CENTER;
                                restaurantName.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                                document.Add(restaurantName);

                                Paragraph title = new Paragraph("Where Food Dreams Come True !");
                                title.Alignment = Element.ALIGN_CENTER;
                                title.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
                                document.Add(title);
                                Paragraph restaurantAddress = new Paragraph("Johar Town, A-3 Block, Lahore");
                                restaurantAddress.Alignment = Element.ALIGN_CENTER;
                                restaurantAddress.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.GRAY);
                                document.Add(restaurantAddress);
                                Paragraph heading = new Paragraph("Order History Report");
                                heading.Alignment = Element.ALIGN_CENTER;
                                heading.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.DARK_GRAY);
                                document.Add(heading);
                                document.Add(new Paragraph("\n"));
                                LineSeparator line1 = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1);
                                document.Add(line1);

                                document.Add(new Paragraph("\n"));
                                document.Add(new Paragraph($"Date: {DateTime.Today.ToShortDateString()}"));
                                document.Add(new Paragraph($"Time: {DateTime.Now.ToShortTimeString()}"));
                                document.Add(new Paragraph("Report Type: Order Invoice"));
                                document.Add(new Paragraph("Status: Paid"));
                                document.Add(new Paragraph("Printed By: Manager"));

                                document.Add(new Paragraph("\n"));
                                PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    table.AddCell(new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText))
                                    {
                                        HorizontalAlignment = Element.ALIGN_CENTER
                                    });
                                }

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

                                document.Add(table);
                                document.Add(new Paragraph("\n"));
                                Paragraph P = new Paragraph($"Total: {label5.Text}");
                                document.Add(P);
                                Paragraph P1 = new Paragraph("Tax: 16%");
                                document.Add(P1);
                                Paragraph P2 = new Paragraph($"Net Total: {label7.Text}");
                                document.Add(P2);
                                document.Close();

                                MessageBox.Show("Order punched and Bill saved successfully !");

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        try
                        {
                            var con = Configuration.getInstance().getConnection();
                           

                            string insertQuery = "INSERT INTO dbo.[orders] (Quantity,Type,Amount,Staff,Status,Active,CreatedAt, UpdatedAt,Customer,Address,Items) VALUES (@A, @B, @C, @D, @E, @F, @G, @H,@I,@J,@K)";
                            SqlCommand command = new SqlCommand(insertQuery, con);
                            command.Parameters.AddWithValue("@A", dataGridView1.Rows.Count);
                            command.Parameters.AddWithValue("@B", orderType); 
                            command.Parameters.AddWithValue("@C", label7.Text);
                            if(orderType=="Dine In")
                            {
                            command.Parameters.AddWithValue("@D", "Waiter");
                                command.Parameters.AddWithValue("@J","Not Required");
                            }
                            else if(orderType=="Delivery")
                            {  
                                command.Parameters.AddWithValue("@D", "Delivery Boy");
                                command.Parameters.AddWithValue("@J",txtAddress.Text.ToString());

                            }
                            else
                            {
                                command.Parameters.AddWithValue("@D", "Self Service");
                                command.Parameters.AddWithValue("@J", "Not Required");

                            }
                            command.Parameters.AddWithValue("@E", "Paid");
                            command.Parameters.AddWithValue("@F", false);
                            command.Parameters.AddWithValue("@G", DateTime.Now);
                            command.Parameters.AddWithValue("@H", DateTime.Now);
                            command.Parameters.AddWithValue("@I",txtName.Text.ToString());
                            string columnName = "Name";
                            string valuesList = string.Join(",", dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => "1 " + (row.Cells[columnName].Value?.ToString() ?? "")));
                            command.Parameters.AddWithValue("@K", valuesList);
                            int rowsAffected = command.ExecuteNonQuery();
                            button6_Click(null, null);

                        }
                        catch (Exception ex)
                        {
                            LogExceptionToDatabase(ex);
                            MessageBox.Show(ex.Message);
                        }


                    }


                }
                else
                {
                    string savePath;

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF Files|*.pdf";
                        try
                        {
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                savePath = saveFileDialog.FileName;
                                Document document = new Document();
                                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                                document.Open();
                                Paragraph restaurantInfo = new Paragraph();
                                restaurantInfo.Alignment = Element.ALIGN_CENTER;
                                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("D:\\SEMESTER 5\\SE LAB\\Developer\\Project\\Git Project\\Restuarant App\\Restuarant App\\Resources\\Restaurant-logo-with-chef-drawing-template-on-transparent-background-PNG.png");
                                logo.ScalePercent(2f);
                                restaurantInfo.Add(logo);
                                restaurantInfo.Alignment = Element.ALIGN_CENTER;
                                document.Add(restaurantInfo);
                                document.Add(new Paragraph("\n"));
                                LineSeparator line = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1);
                                document.Add(line);
                                Paragraph restaurantName = new Paragraph("Restaurant Management System");
                                restaurantName.Alignment = Element.ALIGN_CENTER;
                                restaurantName.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                                document.Add(restaurantName);

                                Paragraph title = new Paragraph("Where Food Dreams Come True !");
                                title.Alignment = Element.ALIGN_CENTER;
                                title.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
                                document.Add(title);
                                Paragraph restaurantAddress = new Paragraph("Johar Town, A-3 Block, Lahore");
                                restaurantAddress.Alignment = Element.ALIGN_CENTER;
                                restaurantAddress.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.GRAY);
                                document.Add(restaurantAddress);
                                Paragraph heading = new Paragraph("Order History Report");
                                heading.Alignment = Element.ALIGN_CENTER;
                                heading.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.DARK_GRAY);
                                document.Add(heading);
                                document.Add(new Paragraph("\n"));
                                LineSeparator line1 = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1);
                                document.Add(line1);

                                document.Add(new Paragraph("\n"));
                                document.Add(new Paragraph($"Date: {DateTime.Today.ToShortDateString()}"));
                                document.Add(new Paragraph($"Time: {DateTime.Now.ToShortTimeString()}"));
                                document.Add(new Paragraph("Report Type: Order Invoice"));
                                document.Add(new Paragraph("Status: Paid"));
                                document.Add(new Paragraph("Printed By: Manager"));

                                document.Add(new Paragraph("\n"));
                                PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    table.AddCell(new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText))
                                    {
                                        HorizontalAlignment = Element.ALIGN_CENTER
                                    });
                                }
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
                                document.Add(table);
                                document.Add(new Paragraph("\n"));
                                Paragraph P = new Paragraph($"Total: {label5.Text}");
                                document.Add(P);
                                Paragraph P1 = new Paragraph("Tax: 16%");
                                document.Add(P1);
                                Paragraph P2 = new Paragraph($"Net Total: {label7.Text}");
                                document.Add(P2);
                                document.Close();
                                MessageBox.Show("Order Punched and Bill saved successfully !");

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        try
                        {
                            var con = Configuration.getInstance().getConnection();
                            string insertQuery = "INSERT INTO dbo.[orders] (Quantity,Type,Amount,Staff,Status,Active,CreatedAt, UpdatedAt,Customer,Address,Items) VALUES (@A, @B, @C, @D, @E, @F, @G, @H,@I,@J,@K)";
                            SqlCommand command = new SqlCommand(insertQuery, con);
                            command.Parameters.AddWithValue("@A", dataGridView1.Rows.Count);
                            command.Parameters.AddWithValue("@B", orderType); // Set CategoryId here
                            command.Parameters.AddWithValue("@C", label7.Text);
                            if (orderType == "Dine In")
                            {
                                command.Parameters.AddWithValue("@D", "Waiter");
                                command.Parameters.AddWithValue("@J", "Not Required");

                            }
                            else if (orderType == "Delivery")
                            {
                                command.Parameters.AddWithValue("@D", "Delivery Boy");
                                command.Parameters.AddWithValue("@J", txtAddress.Text.ToString());

                            }
                            else
                            {
                                command.Parameters.AddWithValue("@D", "Self Service");
                                command.Parameters.AddWithValue("@J", "Not Required");

                            }
                            command.Parameters.AddWithValue("@E", "Unpaid");
                            command.Parameters.AddWithValue("@F", true);
                            command.Parameters.AddWithValue("@G", DateTime.Now);
                            command.Parameters.AddWithValue("@H", DateTime.Now);
                            command.Parameters.AddWithValue("@I", txtAddress.Text.ToString());
                            string columnName = "Name";
                            string valuesList = string.Join(",", dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => "1 " + (row.Cells[columnName].Value?.ToString() ?? "")));
                            command.Parameters.AddWithValue("@K", valuesList);
                            int rowsAffected = command.ExecuteNonQuery();
                            button6_Click(null, null);

                        }
                        catch (Exception ex)
                        {
                            LogExceptionToDatabase(ex);
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
            }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

     

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true; 
                }
            }
            

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && label7.Text!="0")
            {
                decimal remain = decimal.Parse(textBox1.Text) - decimal.Parse(label7.Text);
               
                label12.Text = remain.ToString();
            }

        }
    }
}
