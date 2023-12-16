using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductUpdateLanguageDTO
    {
        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSubTitle { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
    }
}
