﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Domain.Model
{
    public class SalesCombination
    {
        public int SalesCombinationId { get; set; }
        public int MainProductId { get; set; }
        public int SubProductId { get; set; }
        public decimal Discount { get; set; }
    }
}
