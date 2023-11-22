using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;

namespace Restuarant_App
{
    public partial class AdminMain : Form
    {
        public string loginTime;
        public string name;


        public AdminMain(string time, string name)
        {
            InitializeComponent();
            this.loginTime = time;
            this.name = name;
        }

        private void AdminMain_Load(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.BorderSize = 0;
            button7.FlatAppearance.BorderSize = 0;
            button8.FlatAppearance.BorderSize = 0;
            button9.FlatAppearance.BorderSize = 0;
            button10.FlatAppearance.BorderSize = 0;
            button12.FlatAppearance.BorderSize = 0;
            button11.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.BorderSize = 0;
            addControls(new Home());


        }

        private void button5_Click(object sender, EventArgs e)
        {
            addControls(new Tables());
            label3.Text = "Tables";


        }
        private void button1_Click(object sender, EventArgs e)
        {
            addControls(new Dashboard());
            label3.Text = "Dashboard";
        }
        public void addControls(Form F)
        {
            centerPanel.Controls.Clear();
            F.Dock=DockStyle.Fill;
            F.TopLevel = false;
            centerPanel.Controls.Add( F );
            F.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            addControls(new Categories());
            label3.Text = "Food Categories";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            addControls(new Menu());
            label3.Text = "Menu";


        }

        private void button4_Click(object sender, EventArgs e)
        {
            addControls(new Orders());
            label3.Text = "Orders";


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Pos P = new Pos();
            P.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            addControls(new Staff());
            label3.Text = "Staff";


        }

        private void button8_Click(object sender, EventArgs e)
        {
            addControls(new Reports());
            label3.Text = "Reports";


        }

        private void button9_Click(object sender, EventArgs e)
        {
            addControls(new Profile(loginTime,name));
            label3.Text = "My Profile";


        }
        private void button10_Click(object sender, EventArgs e)
        {
            addControls(new Home());
            label3.Text = "Home";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            addControls(new Account());
            label3.Text="Add Admin Account";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            addControls(new Feeback());
            label3.Text = "Feedback";
        }
    }
}
