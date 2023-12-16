using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class PraductIsFicertDTO
    {

        public int Id { get; set; }
        public bool IsFeatured { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public string SeoUrl { get; set;}
    }
}
