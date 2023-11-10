using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarant_App._BL
{
    class CategoriesBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CategoriesBL(string name, string type, bool active, DateTime createdAt, DateTime updatedAt)
        {
            Name = name;
            Type = type;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }


}
