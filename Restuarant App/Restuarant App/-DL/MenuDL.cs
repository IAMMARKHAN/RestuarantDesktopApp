using db2021finalprojectg_9;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restuarant_App._BL;
using System.Windows.Forms;

namespace Restuarant_App._DL
{
    public class MenuDL
    {
        private List<MenuBL> menuItems = new List<MenuBL>();
        public List<MenuBL> MenuItems
        {
            get { return menuItems; }
        }

        private void LoadMenuItems()
            {
                var con = Configuration.getInstance().getConnection();

                string query = "SELECT * FROM dbo.[menu]";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MenuBL menuItem = new MenuBL
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            Price = Convert.ToInt32(reader["Price"]),
                            Size = reader["Size"].ToString(),
                            ImageData = (byte[])reader["ImageData"], 
                            Active = Convert.ToBoolean(reader["Active"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                        };

                        menuItems.Add(menuItem);
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw;
                }
                finally
                {
                    con.Close(); 
                }
            }

          

            
        }

    }

