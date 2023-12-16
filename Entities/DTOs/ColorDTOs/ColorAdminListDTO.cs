using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ColorDTOs
{
    public class ColorAdminListDTO
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public bool IsFeatured { get; set; }
    }
}
