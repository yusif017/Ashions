using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ColorDTOs
{
    public class ColorAddDTO
    {
       
        public List<string> ColorName { get; set; }
        public List<string> LangCode { get; set; }
    }
}
