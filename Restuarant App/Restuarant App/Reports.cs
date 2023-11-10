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
                    Paragraph title = new Paragraph("Restaurant Management System");
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    Paragraph heading = new Paragraph("Order History Report");
                    heading.Alignment = Element.ALIGN_CENTER;
                    document.Add(heading);
                    document.Add(new Paragraph("\n"));
                    PdfPTable timeAndDayTable = new PdfPTable(2);
                    timeAndDayTable.WidthPercentage = 100;
                    PdfPCell timeCell = new PdfPCell(new Phrase($"Time: {DateTime.Now.ToString("hh:mm tt")}"));
                    timeCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    timeAndDayTable.AddCell(timeCell);
                    PdfPCell dayCell = new PdfPCell(new Phrase($"Day: {DateTime.Now.ToString("dddd, MMMM dd, yyyy")}"));
                    dayCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    timeAndDayTable.AddCell(dayCell);
                    document.Add(timeAndDayTable);
                    document.Add(new Paragraph("\n"));
                    DataTable staffData = FetchOrderTableDataFromDatabase();
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
                    Paragraph title = new Paragraph("Restaurant Management System");
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    Paragraph heading = new Paragraph("Active Staff Report");
                    heading.Alignment = Element.ALIGN_CENTER;
                    document.Add(heading);
                    document.Add(new Paragraph("\n"));
                    PdfPTable timeAndDayTable = new PdfPTable(2);
                    timeAndDayTable.WidthPercentage = 100;
                    PdfPCell timeCell = new PdfPCell(new Phrase($"Time: {DateTime.Now.ToString("hh:mm tt")}"));
                    timeCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    timeAndDayTable.AddCell(timeCell);
                    PdfPCell dayCell = new PdfPCell(new Phrase($"Day: {DateTime.Now.ToString("dddd, MMMM dd, yyyy")}"));
                    dayCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    timeAndDayTable.AddCell(dayCell);
                    document.Add(timeAndDayTable);
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
                    Paragraph title = new Paragraph("Restaurant Management System");
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    Paragraph heading = new Paragraph("Active Menu Report");
                    heading.Alignment = Element.ALIGN_CENTER;
                    document.Add(heading);
                    document.Add(new Paragraph("\n"));
                    PdfPTable timeAndDayTable = new PdfPTable(2);
                    timeAndDayTable.WidthPercentage = 100;
                    PdfPCell timeCell = new PdfPCell(new Phrase($"Time: {DateTime.Now.ToString("hh:mm tt")}"));
                    timeCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    timeAndDayTable.AddCell(timeCell);
                    PdfPCell dayCell = new PdfPCell(new Phrase($"Day: {DateTime.Now.ToString("dddd, MMMM dd, yyyy")}"));
                    dayCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    timeAndDayTable.AddCell(dayCell);
                    document.Add(timeAndDayTable);
                    document.Add(new Paragraph("\n"));
                    DataTable staffData = FetchMenuTableDataFromDatabase();
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
                    Paragraph title = new Paragraph("Restaurant Management System");
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    Paragraph heading = new Paragraph("Dine In History Report");
                    heading.Alignment = Element.ALIGN_CENTER;
                    document.Add(heading);
                    document.Add(new Paragraph("\n"));
                    PdfPTable timeAndDayTable = new PdfPTable(2);
                    timeAndDayTable.WidthPercentage = 100;
                    PdfPCell timeCell = new PdfPCell(new Phrase($"Time: {DateTime.Now.ToString("hh:mm tt")}"));
                    timeCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    timeAndDayTable.AddCell(timeCell);
                    PdfPCell dayCell = new PdfPCell(new Phrase($"Day: {DateTime.Now.ToString("dddd, MMMM dd, yyyy")}"));
                    dayCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    timeAndDayTable.AddCell(dayCell);
                    document.Add(timeAndDayTable);
                    document.Add(new Paragraph("\n"));
                    DataTable staffData = FetchDineInTableDataFromDatabase();
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
            else
            {
            }
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
