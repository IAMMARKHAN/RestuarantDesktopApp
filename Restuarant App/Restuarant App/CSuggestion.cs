using db2021finalprojectg_9;
using Restuarant_App._BL;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Restuarant_App
{
    public partial class CSuggestion : Form
    {
        public int cid;
        public string name;
        public int btnClick=0;
        public CSuggestion(int cid,string name)
        {
            InitializeComponent();
            this.cid = cid;
            this.name = name;
        }

        private void CSuggestion_Load(object sender, EventArgs e)
        {
            label1.Text = "Hey " + name + " "+ "! We Appreciate Your Suggestions";
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text.Length > 0)
            {
            SuggestionBL S = new SuggestionBL(cid,richTextBox1.Text,DateTime.Now,DateTime.Now);
                string query = "INSERT INTO suggestion (CustomerId, Suggesstion, CreatedAt, UpdatedAt) VALUES (@CustomerId, @Suggestion, @CreatedAt, @UpdatedAt);";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@CustomerId", S.CustomerId);
                command.Parameters.AddWithValue("@Suggestion", S.SuggestionText);
                command.Parameters.AddWithValue("@CreatedAt", S.CreatedAt); 
                command.Parameters.AddWithValue("@UpdatedAt", S.UpdatedAt); 
                command.ExecuteNonQuery();
                MessageBox.Show("Thank You ! Your Suggestion Recorded !");
                richTextBox1.Text = "";

            }
            else
            {

            MessageBox.Show("Enter Suggestion First !");
        }
            }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("We are really sorry to hear this ! Give us another chance");
            btnClick = 1;
            timer1.Interval = 100; 
            timer1.Start();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thang You ! We will try to improve");
            btnClick = 1;
            timer1.Interval = 100; 
            timer1.Start();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thang You ! Good to know this");
            btnClick = 1;
            timer1.Interval = 100; 
            timer1.Start();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you ! We are glad to serve you ");
            btnClick = 1;
            timer1.Interval = 100; 
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (btnClick == 1)
            {
                tableLayoutPanel3.Controls.Clear();
                timer1.Stop();
                timer1.Dispose();
                tableLayoutPanel3.BackColor = Color.Transparent ;
            }
        }
    }
}
