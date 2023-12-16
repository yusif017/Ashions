﻿using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductAdminListDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string CategoryName { get; set; }
        public bool IsFeatured { get; set; }
        public int Quantity { get; set; }
        public decimal KargoPrice { get; set; }
        public PraductSize PraductSize { get; set; }

        public ProductKargo ProductKargo { get; set; }
    }
}
