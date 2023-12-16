﻿using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSubTitle { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int CategoryId { get; set; }
        public int PraductOxsarId { get; set; }
        public string ColorName { get; set; }
        public PraductSize PraductSize { get; set; }
        public ProductKargo ProductKargo { get; set; }
        public decimal KargoPrice { get; set; }

        public int Quantity { get; set; }
        public List<string> PhotoUrls { get; set; }
        
    }
}
