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
using iTextSharp.text.pdf.draw;

namespace Restuarant_App
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to print this report?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save Pdf File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog.FileName;
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
                   
                    document.Add(new Paragraph("\n"));
                    LineSeparator line1 = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1);
                    document.Add(line1);
                    document.Add(new Paragraph("\n"));
                    Paragraph heading = new Paragraph("Order History Report");
                    heading.Alignment = Element.ALIGN_CENTER;
                    heading.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.DARK_GRAY);
                    document.Add(heading);
                    document.Add(new Paragraph("\n"));
                    Paragraph timeAndDayParagraph = new Paragraph();
                    timeAndDayParagraph.Alignment = Element.ALIGN_CENTER;

                    Chunk timeChunk = new Chunk($"Time: {DateTime.Now.ToString("hh:mm tt")}");
                    timeAndDayParagraph.Add(timeChunk);

                    Chunk separatorChunk = new Chunk("   |   ", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DARK_GRAY));
                    separatorChunk.SetAnchor(new Uri("about:blank")); 
                    timeAndDayParagraph.Add(separatorChunk);

                    Chunk dayChunk = new Chunk($"Day: {DateTime.Now.ToString("dddd, MMMM dd, yyyy")}");
                    timeAndDayParagraph.Add(dayChunk);

                    document.Add(timeAndDayParagraph);
                    document.Add(new Paragraph("\n"));





                    DataTable orderData = FetchOrderTableDataFromDatabase();
                    if (orderData != null && orderData.Rows.Count > 0)
                    {
                        PdfPTable table = new PdfPTable(orderData.Columns.Count);
                        table.WidthPercentage = 100;

                        foreach (DataColumn column in orderData.Columns)
                        {
                            PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName));
                            headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(headerCell);
                        }

                        foreach (DataRow row in orderData.Rows)
                        {
                            foreach (object item in row.ItemArray)
                            {
                                table.AddCell(item.ToString());
                            }
                        }

                        document.Add(table);
                        document.Close();
                        MessageBox.Show("Pdf Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Data Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to print this report?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save Pdf File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog.FileName;
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
                    document.Add(new Paragraph("\n"));
                    LineSeparator line1 = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1);
                    document.Add(line1);
                    document.Add(new Paragraph("\n"));
                    Paragraph heading = new Paragraph("Staff History Report");
                    heading.Alignment = Element.ALIGN_CENTER;
                    heading.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.DARK_GRAY);
                    document.Add(heading);


                    Paragraph timeAndDayParagraph = new Paragraph();
                    timeAndDayParagraph.Alignment = Element.ALIGN_CENTER;

                    Chunk timeChunk = new Chunk($"Time: {DateTime.Now.ToString("hh:mm tt")}");
                    timeAndDayParagraph.Add(timeChunk);

                    Chunk separatorChunk = new Chunk("   |   ", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DARK_GRAY));
                    separatorChunk.SetAnchor(new Uri("about:blank"));
                    timeAndDayParagraph.Add(separatorChunk);

                    Chunk dayChunk = new Chunk($"Day: {DateTime.Now.ToString("dddd, MMMM dd, yyyy")}");
                    timeAndDayParagraph.Add(dayChunk);

                    document.Add(timeAndDayParagraph);
                    document.Add(new Paragraph("\n"));

                    DataTable staffData = FetchStaffTableDataFromDatabase();
                    if (staffData != null && staffData.Rows.Count > 0)
                    {
                        PdfPTable table = new PdfPTable(staffData.Columns.Count);
                        table.WidthPercentage = 100;
                        foreach (DataColumn column in staffData.Columns)
                        {
                            PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName));
                            headerCell.BackgroundColor = BaseColor.DARK_GRAY;
                            table.AddCell(headerCell);
                        }
                        foreach (DataRow row in staffData.Rows)
                        {
                            foreach (object item in row.ItemArray)
                            {
                                table.AddCell(item.ToString());
                            }
                        }
                        document.Add(table);
                        document.Close();
                        MessageBox.Show("Pdf Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Data Found !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to print this report?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save Pdf File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog.FileName;
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
                    document.Add(new Paragraph("\n"));
                    LineSeparator line1 = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1);
                    document.Add(line1);
                    document.Add(new Paragraph("\n"));
                    Paragraph heading = new Paragraph("Menu Items Report");
                    heading.Alignment = Element.ALIGN_CENTER;
                    heading.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.DARK_GRAY);
                    document.Add(heading);
                    document.Add(new Paragraph("\n"));


                    Paragraph timeAndDayParagraph = new Paragraph();
                    timeAndDayParagraph.Alignment = Element.ALIGN_CENTER;

                    Chunk timeChunk = new Chunk($"Time: {DateTime.Now.ToString("hh:mm tt")}");
                    timeAndDayParagraph.Add(timeChunk);

                    Chunk separatorChunk = new Chunk("   |   ", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DARK_GRAY));
                    separatorChunk.SetAnchor(new Uri("about:blank"));
                    timeAndDayParagraph.Add(separatorChunk);

                    Chunk dayChunk = new Chunk($"Day: {DateTime.Now.ToString("dddd, MMMM dd, yyyy")}");
                    timeAndDayParagraph.Add(dayChunk);

                    document.Add(timeAndDayParagraph);
                    document.Add(new Paragraph("\n"));

                    DataTable menuData = FetchMenuTableDataFromDatabase();
                    if (menuData != null && menuData.Rows.Count > 0)
                    {
                        PdfPTable table = new PdfPTable(menuData.Columns.Count - 1); 
                        table.WidthPercentage = 100;

                        foreach (DataColumn column in menuData.Columns)
                        {
                            if (column.ColumnName != "Image")  
                            {
                                PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName));
                                headerCell.BackgroundColor = BaseColor.DARK_GRAY;
                                table.AddCell(headerCell);
                            }
                        }

                        foreach (DataRow row in menuData.Rows)
                        {
                            foreach (DataColumn column in menuData.Columns)
                            {
                                if (column.ColumnName != "Image")  
                                {
                                    table.AddCell(row[column].ToString());
                                }
                            }
                        }

                        document.Add(table);
                        document.Close();
                        MessageBox.Show("Pdf Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Data Found !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to print this report?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save Pdf File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog.FileName;
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
                    document.Add(new Paragraph("\n"));
                    LineSeparator line1 = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1);
                    document.Add(line1);
                    document.Add(new Paragraph("\n"));
                    Paragraph heading = new Paragraph("Daily Sales Report");
                    heading.Alignment = Element.ALIGN_CENTER;
                    heading.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.DARK_GRAY);
                    document.Add(heading);
                    document.Add(new Paragraph("\n"));

                    Paragraph timeAndDayParagraph = new Paragraph();
                    timeAndDayParagraph.Alignment = Element.ALIGN_CENTER;

                    Chunk timeChunk = new Chunk($"Time: {DateTime.Now.ToString("hh:mm tt")}");
                    timeAndDayParagraph.Add(timeChunk);

                    Chunk separatorChunk = new Chunk("   |   ", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DARK_GRAY));
                    separatorChunk.SetAnchor(new Uri("about:blank"));
                    timeAndDayParagraph.Add(separatorChunk);

                    Chunk dayChunk = new Chunk($"Day: {DateTime.Now.ToString("dddd, MMMM dd, yyyy")}");
                    timeAndDayParagraph.Add(dayChunk);

                    document.Add(timeAndDayParagraph);
                    document.Add(new Paragraph("\n"));


                    DataTable ordersData = FetchOrdersTableDataFromDatabase();

                    if (ordersData != null && ordersData.Rows.Count > 0)
                    {
                        Dictionary<DateTime, int> salesCountByDay = new Dictionary<DateTime, int>();

                        foreach (DataRow orderRow in ordersData.Rows)
                        {
                            if (orderRow["CreatedAt"] != DBNull.Value && orderRow["CreatedAt"] != null)
                            {
                                DateTime orderDate = ((DateTime)orderRow["CreatedAt"]).Date;

                                if (salesCountByDay.ContainsKey(orderDate))
                                    salesCountByDay[orderDate]++;
                                else
                                    salesCountByDay[orderDate] = 1;
                            }
                        }

                        PdfPTable salesTable = new PdfPTable(4);
                        salesTable.WidthPercentage = 100;

                        PdfPCell dateHeader = new PdfPCell(new Phrase("Date"));
                        dateHeader.BackgroundColor = BaseColor.DARK_GRAY;
                        salesTable.AddCell(dateHeader);

                        PdfPCell dayHeader = new PdfPCell(new Phrase("Day"));
                        dayHeader.BackgroundColor = BaseColor.DARK_GRAY;
                        salesTable.AddCell(dayHeader);

                        PdfPCell salesCountHeader = new PdfPCell(new Phrase("Sales Count"));
                        salesCountHeader.BackgroundColor = BaseColor.DARK_GRAY;
                        salesTable.AddCell(salesCountHeader);

                        PdfPCell statusHeader = new PdfPCell(new Phrase("Status"));
                        statusHeader.BackgroundColor = BaseColor.DARK_GRAY;
                        salesTable.AddCell(statusHeader);

                        foreach (var entry in salesCountByDay)
                        {
                            DateTime date = entry.Key;
                            string day = date.ToString("dddd");
                            int salesCount = entry.Value;
                            string status = (salesCount > 5) ? "Productive Day" : "Average Day";

                            salesTable.AddCell(date.ToString("yyyy-MM-dd"));
                            salesTable.AddCell(day);
                            salesTable.AddCell(salesCount.ToString());
                            salesTable.AddCell(status);
                        }

                        document.Add(salesTable);
                        document.Close();
                        MessageBox.Show("Pdf Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Data Found !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }




        }
       

        private DataTable FetchOrdersTableDataFromDatabase()
        {
            DataTable ordersData = new DataTable();

            try
            {
                string query = "SELECT * FROM [restuarant].[dbo].[orders]";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ordersData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching orders data: " + ex.Message);
            }

            return ordersData;
        }


        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private DataTable FetchOrderTableDataFromDatabase()
        {
            var con = Configuration.getInstance().getConnection();
            DataTable staffData = new DataTable();

            string query = "SELECT Quantity,Type,Items,Amount,Status,Active,CreatedAt,Customer,Address FROM dbo.[orders]";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(staffData);
                return staffData;
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show($"Error fetching data from the database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
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
        private DataTable FetchStaffTableDataFromDatabase()
        {
            var con = Configuration.getInstance().getConnection();
            DataTable staffData = new DataTable();

            string query = "SELECT * FROM dbo.[staff]";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(staffData);
                return staffData;
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show($"Error fetching data from the database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private DataTable FetchDineInTableDataFromDatabase()
        {
            var con = Configuration.getInstance().getConnection();
            DataTable staffData = new DataTable();

            string query = "SELECT Quantity,Type,Items,Amount,Staff,Status,CreatedAt,Customer,Address FROM dbo.[orders] WHERE Type = 'Dine In'";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(staffData);
                return staffData;
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show($"Error fetching data from the database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private DataTable FetchMenuTableDataFromDatabase()
        {
            var con = Configuration.getInstance().getConnection();
            DataTable staffData = new DataTable();

            string query = "SELECT * FROM dbo.[menu]";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(staffData);
                return staffData;
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show($"Error fetching data from the database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
