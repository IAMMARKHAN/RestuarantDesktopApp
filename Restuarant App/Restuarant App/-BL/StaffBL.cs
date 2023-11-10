using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarant_App._BL
{
    public class StaffBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Contact { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public StaffBL( string name, string type, double contact, bool active, DateTime createdAt, DateTime updatedAt)
        {
            Name = name;
            Type = type;
            Contact = contact;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public StaffBL()
        {

        }
    }
}
