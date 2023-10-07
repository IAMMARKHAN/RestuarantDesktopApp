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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Restuarant_App
{
    public partial class CSuggestion : Form
    {
        public int cid;
        public CSuggestion(int cid)
        {
            InitializeComponent();
            this.cid = cid;
        }

        private void CSuggestion_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text.Length > 0)
            {
                string query = "INSERT INTO suggestion (CustomerId, Suggesstion, CreatedAt, UpdatedAt) VALUES (@CustomerId, @Suggestion, @CreatedAt, @UpdatedAt);";
                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@CustomerId", cid); // replace with the name of your username input textbox
                command.Parameters.AddWithValue("@Suggestion", richTextBox1.Text);
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now); // replace with the name of your password input textbox
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now); // replace with the name of your password input textbox
                command.ExecuteNonQuery();
                MessageBox.Show("Thank You ! Your Suggestion Recorded !");

            }
            else
            {

            MessageBox.Show("Enter Suggestion First !");
        }
            }
    }
}
