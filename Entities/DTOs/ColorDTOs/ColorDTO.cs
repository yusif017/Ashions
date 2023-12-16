using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ColorDTOs
{
    public class ColorDTO
    {
        public record ColorFeaturedDTO(int Id, string ColorName, string SeoUrl);

    }
}
