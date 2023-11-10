using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarant_App._BL
{
    class TablesBL
    {
        public int Id { get; set; }
        public int Seats { get; set; }
        public string Located { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public TablesBL( int seats, string located, bool active, DateTime createdAt, DateTime updatedAt)
        {
            Seats = seats;
            Located = located;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
