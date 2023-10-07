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
    public partial class CProfile : Form
    {
    public string time;
    public string name;
        public CProfile(string tiime, string name)
        {
            InitializeComponent();
            this.time = tiime;
            this.name = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm != this) // Exclude the current form (the one with the button)
                {
                    openForm.Hide();
                }
            }
            Login L = new Login();
            L.Show();
        }

        private void CProfile_Load(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderSize = 0;
            label3.Text = time;
            label8.Text = name;
        }
    }
}
