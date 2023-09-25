using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_App
{
    public partial class CustomerMain : Form
    {
        public CustomerMain()
        {
            InitializeComponent();
        }
        public void addControls(Form F)
        {
            panel3.Controls.Clear();
            F.Dock = DockStyle.Fill;
            F.TopLevel = false;
            panel3.Controls.Add(F);
            F.Show();
        }
        private void CustomerMain_Load(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.BorderSize = 0;
            button7.FlatAppearance.BorderSize = 0;
            button10.FlatAppearance.BorderSize = 0;

            button1.FlatAppearance.BorderSize = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addControls(new CMenu());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            addControls(new CDeals());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            addControls(new CReserve());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            addControls(new COrders());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            addControls(new CSuggestion());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            addControls(new CCart());

        }

        private void button7_Click(object sender, EventArgs e)
        {
            addControls(new CProfile());

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            addControls(new Home());
        }
    }
}
