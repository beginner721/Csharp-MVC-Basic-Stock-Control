using System;
using System.Collections.Generic;

#nullable disable

namespace MvcBasicStockControl.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
