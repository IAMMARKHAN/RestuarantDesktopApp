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
    public partial class CFavourites : Form
    {
        public static List<CartItem> cartItems = new List<CartItem>();

        public CFavourites()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        public void PopulateCartItems(List<CartItem> cartItems)
        {
            dataGridView2.Rows.Clear();
            foreach (var item in cartItems)
            {
                dataGridView2.Rows.Add(item.Id, item.Name, item.Price, item.Size);
            }
        }

        private void CFavourites_Load(object sender, EventArgs e)
        {
            PopulateCartItems(cartItems);   
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].Name == "Add")
            {
                DialogResult result = MessageBox.Show("Add This Item To Cart ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                    string idColumnName = "Id";
                    string nameColumnName = "Name";
                    string priceColumnName = "Price";
                    string sizeColumnName = "Size";
                    int idValue = Convert.ToInt32(row.Cells[idColumnName].Value);
                    string nameValue = row.Cells[nameColumnName].Value.ToString();
                    int priceValue = Convert.ToInt32(row.Cells[priceColumnName].Value);
                    string sizeValue = row.Cells[sizeColumnName].Value.ToString();
                    CCart.cartItems.Add(new CartItem
                    {
                        Id = idValue,
                        Name = nameValue,
                        Price = priceValue,
                        Size = sizeValue
                    });
                    MessageBox.Show("Added Successfully !", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }

            }

        }
    }
}
