﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductFilteredDTO
    {
        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string PhotoUrl { get; set; }
        public string SeoUrl { get; set; }

    }
}
