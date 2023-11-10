using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarant_App._BL
{
    public class MenuBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public byte[] ImageData { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public MenuBL()
        {
        }
        public MenuBL( string name, int categoryId, int price, string size, byte[] imageData, bool active, DateTime createdAt, DateTime updatedAt)
        {
            Name = name;
            CategoryId = categoryId;
            Price = price;
            Size = size;
            ImageData = imageData;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
