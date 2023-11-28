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
            button1.FlatAppearance.BorderSize = 0;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex>=0 && dateTimePicker1.Checked)
            {
                var con = Configuration.getInstance().getConnection();
                string query = "SELECT COUNT(*) FROM dbo.[tables] WHERE Seats = @A AND Located = @B"; 
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@A", comboBox2.Text);
                command.Parameters.AddWithValue("@B", comboBox1.Text);
                int userCount = Convert.ToInt32(command.ExecuteScalar());
                if (userCount > 0)
                {
                    string tableIdQuery = "SELECT Id FROM dbo.[tables] WHERE Seats = @A AND Located = @B";
                    SqlCommand tableIdCommand = new SqlCommand(tableIdQuery, con);
                    tableIdCommand.Parameters.AddWithValue("@A", comboBox2.Text);
                    tableIdCommand.Parameters.AddWithValue("@B", comboBox1.Text);
                    int tableId = Convert.ToInt32(tableIdCommand.ExecuteScalar());
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
                        TableReservationBL T = new TableReservationBL(cid, tableId, true, DateTime.Now, DateTime.Now,dateTimePicker1.Value,int.Parse(comboBox2.Text));
                        string query22 = "INSERT INTO tableReservation (CustomerId, TableId,Active, CreatedAt, UpdatedAt,Date,Persons) VALUES (@CustomerId, @Suggestion,@Suggestion1, @CreatedAt, @UpdatedAt,@Ad,@F);";
                        var con22 = Configuration.getInstance().getConnection();
                        SqlCommand command22 = new SqlCommand(query22, con22);
                        command22.Parameters.AddWithValue("@CustomerId", T.CustomerId);
                        command22.Parameters.AddWithValue("@Suggestion", T.TableId);
                        command22.Parameters.AddWithValue("@CreatedAt", T.CreatedAt); 
                        command22.Parameters.AddWithValue("@UpdatedAt", T.UpdatedAt);
                        command22.Parameters.AddWithValue("@Ad", T.Date);
                        command22.Parameters.AddWithValue("@F", T.Persons);
                        command22.Parameters.AddWithValue("@Suggestion1", T.Active);
                        command22.ExecuteNonQuery();
                        MessageBox.Show("Your Reservation Confrimed !");
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                        dateTimePicker1.Value = DateTime.Now;
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

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
