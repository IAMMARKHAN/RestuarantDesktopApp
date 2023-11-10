using db2021finalprojectg_9;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Restuarant_App
{
    public partial class Dashboard : Form
    {
        int staff, category, table, order;
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        { 

            chartLine.Titles.Add("");
            chartLine.ChartAreas[0].AxisX.Title = "Reservation Date";
            chartLine.ChartAreas[0].AxisY.Title = "Total Reserved Table";
            Series seriesLine = chartLine.Series.Add("");
            seriesLine.ChartType = SeriesChartType.Point; 
            seriesLine.MarkerStyle = MarkerStyle.Circle; 
            seriesLine.MarkerSize = 20; 
            seriesLine.Color = Color.Red; 

            var con2 = Configuration.getInstance().getConnection();

            string sqlQuery1 = "SELECT CreatedAt, COUNT(*) AS ReservationCount FROM tableReservation GROUP BY CreatedAt ORDER BY CreatedAt";

            SqlCommand cmd2 = new SqlCommand(sqlQuery1, con2);

            try
            {
                SqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    int reservationCount = reader.GetInt32(1);
                    seriesLine.Points.AddXY(date.ToShortDateString(), reservationCount);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            var con = Configuration.getInstance().getConnection();

            string sqlQuery = "SELECT CONVERT(DATE, CreatedAt) AS OrderDate, COUNT(*) AS OrderCount FROM orders GROUP BY CONVERT(DATE, CreatedAt) ORDER BY OrderDate";
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                chartBar.Titles.Add("");
                chartBar.ChartAreas[0].AxisX.Title = "Order Punch Date";
                chartBar.ChartAreas[0].AxisY.Title = "Orders Count";
                Series seriesBar = chartBar.Series.Add("Orders");
                seriesBar.ChartType = SeriesChartType.Area;
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    int orderCount = reader.GetInt32(1);

                    seriesBar.Points.AddXY(date.ToShortDateString(), orderCount);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }



            staff = GetStaffCount();
            order = GetOrdersCount();
            table = GetTableCount();
            category = GetCategoriesCount();
            label6.Text = category.ToString();
            label7.Text = table.ToString();
            label8.Text = staff.ToString();
            label5.Text = order.ToString();

        }
        public int GetTableCount()
        {
            try
            {

                var con = Configuration.getInstance().getConnection();
                string query = "SELECT COUNT(*) FROM dbo.[tables]"; 
                SqlCommand command = new SqlCommand(query, con);
                int userCount = Convert.ToInt32(command.ExecuteScalar());
                return userCount;
            }
            catch(Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show("Error Loading");
            }
           
            return 0;
                
            
        }
        public int GetCategoriesCount()
        {

            try
            {

            var con = Configuration.getInstance().getConnection();
            string query = "SELECT COUNT(*) FROM dbo.[categories]"; 
            SqlCommand command = new SqlCommand(query, con);
            int userCount = Convert.ToInt32(command.ExecuteScalar());
            return userCount;
            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show("Error Loading");
            }
            return 0;


        }
        public int GetStaffCount()
        {
            try
            {

            var con = Configuration.getInstance().getConnection();
            string query = "SELECT COUNT(*) FROM dbo.[staff]"; 
            SqlCommand command = new SqlCommand(query, con);
            int userCount = Convert.ToInt32(command.ExecuteScalar());
            return userCount;

            }
            catch (Exception ex)
            {
                LogExceptionToDatabase(ex);
                MessageBox.Show("Error Loading");
            }
            return 0;


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

        public int GetOrdersCount()
        {

            var con = Configuration.getInstance().getConnection();
            string query = "SELECT COUNT(*) FROM dbo.[orders]";
            SqlCommand command = new SqlCommand(query, con);
            int userCount = Convert.ToInt32(command.ExecuteScalar());
            return userCount;


        }
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void chartBar_Click(object sender, EventArgs e)
        {

        }

        private void chartLine_Click(object sender, EventArgs e)
        {

        }
    }
}
