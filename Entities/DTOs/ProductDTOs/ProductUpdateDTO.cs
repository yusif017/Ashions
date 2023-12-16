﻿using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductUpdateDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal KargoPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int PraductOxsarId { get; set; }
        public int ProductColorId { get; set; }
        public PraductSize PraductSize { get; set; }
        public ProductKargo ProductKargo { get; set; }
        public bool IsFeatured { get; set; }
        public List<string> PhotoUrls { get; set; }
        public List<ProductUpdateLanguageDTO> Lang { get; set; }
    }
}
