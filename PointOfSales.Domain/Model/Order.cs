using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Domain.Model
{
    public class Order
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
