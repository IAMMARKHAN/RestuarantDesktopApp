﻿using db2021finalprojectg_9;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Restuarant_App
{
    public partial class CCart : Form
    {
        public  static List<CartItem> cartItems = new List<CartItem>();
        public string name;
        public string address ;

        public CCart(string name,string add)
        {
            InitializeComponent();
            this.name = name;
            this.address = add;
        }
        public DataGridView DataGridView2
        {
            get { return dataGridView2; }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            cartItems.Clear();
            label5.Text = 0.ToString();
            label7.Text = 0.ToString();
        }
        public void PopulateCartItems(List<CartItem> cartItems)
        {
            dataGridView2.Rows.Clear(); 
            foreach (var item in cartItems)
            {
                // Add a row to the DataGridView
                dataGridView2.Rows.Add(item.Id, item.Name, item.Price, item.Size);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count>0) {
                DialogResult result = MessageBox.Show("Confirm Order ? Payable Amount: " +label7.Text, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result==DialogResult.Yes)
                {                  
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        string insertQuery = "INSERT INTO dbo.[orders] (Quantity,Type,Amount,Staff,Status,Active,CreatedAt, UpdatedAt,Customer,Address,Items) VALUES (@A, @B, @C, @D, @E, @F, @G, @H,@I,@J,@K)";
                        SqlCommand command = new SqlCommand(insertQuery, con);
                        command.Parameters.AddWithValue("@A", dataGridView2.Rows.Count);
                        command.Parameters.AddWithValue("@B", "Delivery"); // Set CategoryId here
                        command.Parameters.AddWithValue("@C", label7.Text);
                        command.Parameters.AddWithValue("@D", "Delivery Boy");
                        command.Parameters.AddWithValue("@E", "Unpaid");
                        command.Parameters.AddWithValue("@F", true);
                        command.Parameters.AddWithValue("@G", DateTime.Now);
                        command.Parameters.AddWithValue("@H", DateTime.Now);
                        command.Parameters.AddWithValue("@I", name);
                        command.Parameters.AddWithValue("@J", address);
                        string columnName = "Name";
                        string valuesList = string.Join(",", dataGridView2.Rows.Cast<DataGridViewRow>().Select(row => "1 " + (row.Cells[columnName].Value?.ToString() ?? "")));
                        command.Parameters.AddWithValue("@K", valuesList);
                        int rowsAffected = command.ExecuteNonQuery();
                        try
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
                                        document.Add(new Paragraph("Report Type: Order Bill"));
                                        document.Add(new Paragraph("Status: Un Paid"));
                                        document.Add(new Paragraph("User : Customer"));

                                        document.Add(new Paragraph("\n"));
                                        // Create a table for the DataGridView content
                                        PdfPTable table = new PdfPTable(dataGridView2.Columns.Count);
                                        for (int i = 0; i < dataGridView2.Columns.Count; i++)
                                        {
                                            table.AddCell(new PdfPCell(new Phrase(dataGridView2.Columns[i].HeaderText))
                                            {
                                                HorizontalAlignment = Element.ALIGN_CENTER
                                            });
                                        }

                                        // Add DataGridView content to the table
                                        foreach (DataGridViewRow row in dataGridView2.Rows)
                                        {
                                            for (int i = 0; i < dataGridView2.Columns.Count; i++)
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
                                        document.Add(P);
                                        Paragraph P1 = new Paragraph("Tax: 16%");
                                        document.Add(P1);
                                        Paragraph P2 = new Paragraph($"Net Total: {label7.Text}");
                                        document.Add(P2);
                                        document.Close();
                                        MessageBox.Show("Order Placed and Bill Saved Successfully !");

                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        cartItems.Clear();
                        label5.Text = 0.ToString();
                        label7.Text = 0.ToString();
                        dataGridView2.Rows.Clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            else
            {
                MessageBox.Show("Add Some Items to Cart !");
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CCart_Load(object sender, EventArgs e)
        {
            PopulateCartItems(cartItems);
            int totalPrice = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["Price"].Value != null && int.TryParse(row.Cells["Price"].Value.ToString(), out int itemPrice))
                {
                    totalPrice += itemPrice;
                }
            }
            label5.Text = totalPrice.ToString();
            decimal tax = (decimal.Parse(label5.Text)) * 0.16m;
            label7.Text = (int.Parse(label5.Text) + tax).ToString();
        }
    }
}
