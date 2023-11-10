using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using db2021finalprojectg_9;
using Restuarant_App._BL;

namespace Restuarant_App._DL
{
    public class OrderDL
    {
        private List<OrdersBL> _orders;

        public List<OrdersBL> Orders
        {
            get { return _orders; }
        }

        public void LoadOrdersFromDatabase()
        {
            var con = Configuration.getInstance().getConnection();
            _orders = new List<OrdersBL>();

            string query = "SELECT Quantity, Type, Items, Amount, Status, Active, CreatedAt, Customer, Address FROM dbo.[orders]";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrdersBL order = new OrdersBL
                    {
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Type = reader["Type"].ToString(),
                        Items = reader["Items"].ToString(),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        Status = reader["Status"].ToString(),
                        Active = Convert.ToBoolean(reader["Active"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        Customer = reader["Customer"].ToString(),
                        Address = reader["Address"].ToString()
                    };

                    _orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
