﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Models
{
    public class ProductPostSave
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
    }
}
