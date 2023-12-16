using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        public int Id { get; set; }
        public List<CategoryUpdateLanguageDTO> CategoryLang { get; set; }
        public bool IsFeatured { get; set; }
        public string PhotoUrl { get; set; }
    }
}
