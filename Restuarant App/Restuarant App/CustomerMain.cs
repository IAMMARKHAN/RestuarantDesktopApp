﻿using System;
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
        public string loginTime;
        public string name;
        public int cid;


        public CustomerMain(string time,string name,int cid)
        {
            InitializeComponent();
            this.loginTime = time;
            this.name = name;
            this.cid= cid;
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
            label3.Text = "Main Menu";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            addControls(new CDeals());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            addControls(new CReserve(name,cid));
            label3.Text = "Table Reservation";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            addControls(new COrders());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            addControls(new CSuggestion(cid,name));
            label3.Text = "Suggestion";


        }

        private void button6_Click(object sender, EventArgs e)
        {
            addControls(new CCart());

        }

        private void button7_Click(object sender, EventArgs e)
        {
            addControls(new CProfile(loginTime,name));
            label3.Text = "My Profile";

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            addControls(new Home());
            label3.Text= "Home";
        }
    }
}
