using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Domain.Model
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
