using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarant_App._BL
{
    class SuggestionBL
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string SuggestionText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public SuggestionBL(int customerId, string suggestion, DateTime createdAt, DateTime updatedAt)
        {
            CustomerId = customerId;
            SuggestionText = suggestion;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
