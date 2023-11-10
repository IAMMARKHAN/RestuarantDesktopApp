using db2021finalprojectg_9;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Restuarant_App._BL;

namespace Restuarant_App._DL
{
    public class StaffDL
    {
        private List<StaffBL> staffList = new List<StaffBL>();

        public List<StaffBL> StaffList
        {
            get { return staffList; }
        }

        public void FetchStaffTableDataFromDatabase()
        {
            var con = Configuration.getInstance().getConnection();

            string query = "SELECT * FROM dbo.[staff]";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StaffBL staff = new StaffBL
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Type = reader["Type"].ToString(),
                        Contact = Convert.ToDouble(reader["Contact"]),
                        Active = Convert.ToBoolean(reader["Active"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };

                    staffList.Add(staff);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
