using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarant_App._BL
{
    class UsersBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UsersBL(string name, string email, string password, string address, string contact, string role, bool active, DateTime createdAt, DateTime updatedAt)
        {
            Name = name;
            Email = email;
            Password = password;
            Address = address;
            Contact = contact;
            Role = role;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
