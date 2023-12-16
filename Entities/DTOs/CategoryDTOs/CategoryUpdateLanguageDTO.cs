using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class CategoryUpdateLanguageDTO
    {
        public string LanngCode { get; set; }
        public string Title { get; set; }
    }
}
