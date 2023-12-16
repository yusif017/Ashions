﻿using Entities.DTOs.ProductDTOs;

namespace WebUI.ViewModels
{
    public class PaginationVM
    {
        public int ProductCount { get; set; }
        public int ProductColorCount { get; set; }

        public IEnumerable<ProductFilteredDTO> Products { get; set; }
    }
}
