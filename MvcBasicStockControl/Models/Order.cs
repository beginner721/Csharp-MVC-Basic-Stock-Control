using System;
using System.Collections.Generic;

#nullable disable

namespace MvcBasicStockControl.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
