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
    public partial class CReserve : Form
    {
       public string name = " ";
        public int cid;
        public CReserve(string name,int cid)
        {
            InitializeComponent();
            this.name=name;
            this.cid = cid;
        }

        private void CReserve_Load(object sender, EventArgs e)
        {
            textBox1.Text = name;
            dateTimePicker1.Checked = false;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex>=0 && dateTimePicker1.Checked)
            {
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT COUNT(*) FROM dbo.[tables] WHERE Seats = @A AND Located = @B"; // Replace 'user' with your table name
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@A", comboBox2.Text);
                command.Parameters.AddWithValue("@B", comboBox1.Text);
                int userCount = Convert.ToInt32(command.ExecuteScalar());

                if (userCount > 0)
                {
                    // Table exists, now let's check the tableReservation table
                    string tableIdQuery = "SELECT Id FROM dbo.[tables] WHERE Seats = @A AND Located = @B";
                    SqlCommand tableIdCommand = new SqlCommand(tableIdQuery, con);
                    tableIdCommand.Parameters.AddWithValue("@A", comboBox2.Text);
                    tableIdCommand.Parameters.AddWithValue("@B", comboBox1.Text);
                    // Execute the tableId query
                    int tableId = Convert.ToInt32(tableIdCommand.ExecuteScalar());
                    // Check if there is an entry in the tableReservation table with the same tableId
                    string reservationQuery = "SELECT COUNT(*) FROM tableReservation WHERE TableId = @TableId And Active=@C ";
                    SqlCommand reservationCommand = new SqlCommand(reservationQuery, con);
                    reservationCommand.Parameters.AddWithValue("@TableId", tableId);
                    reservationCommand.Parameters.AddWithValue("@C", true);
                    int reservationCount = Convert.ToInt32(reservationCommand.ExecuteScalar());

                    if (reservationCount > 0)
                    {
                        MessageBox.Show("Sorry, Reservation Already Exists For This Table.");
                    }
                    else
                    {
                        MessageBox.Show("Your Reservation Confrimed !");
                    }
                }
                else
                {
                    MessageBox.Show("Table Not Exists !");
                }


            }
            else
            {

                MessageBox.Show("Select All The Values !");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
