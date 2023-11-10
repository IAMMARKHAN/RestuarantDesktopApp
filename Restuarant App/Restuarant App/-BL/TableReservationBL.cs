using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarant_App._BL
{
    class TableReservationBL
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime Date { get; set; }
        public int Persons { get; set; }



        public TableReservationBL( int customerId, int tableId, bool active, DateTime createdAt, DateTime updatedAt, DateTime date, int person)
        {
            CustomerId = customerId;
            TableId = tableId;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Date = date;
            Persons = person;
        }
    }
}
