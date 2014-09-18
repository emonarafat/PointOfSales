using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Domain.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
