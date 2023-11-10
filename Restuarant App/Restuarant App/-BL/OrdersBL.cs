using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarant_App._BL
{
    public class OrdersBL
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Staff { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Items { get; set; }

        public OrdersBL()
        {

        }
        public OrdersBL(int id, int quantity, string type, decimal amount, string staff, string status, bool active, DateTime createdAt, DateTime updatedAt, string customer, string address, string items)
        {
            Id = id;
            Quantity = quantity;
            Type = type;
            Amount = amount;
            Staff = staff;
            Status = status;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Customer = customer;
            Address = address;
            Items = items;
        }
    }
}
