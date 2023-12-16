using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ColorDTOs
{
    public class ColorUpdateDTO
    {
        public int Id { get; set; }
        public List<ColorUpdateLanguageDTO> ColorLang { get; set; }
        public bool IsFeatured { get; set; }
    }
}
