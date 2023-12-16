using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ColorDTOs;
using Entities.DTOs.ProductDTOs;

namespace WebUI.ViewModels
{
    public class ProductFilterVM
    {
        public IEnumerable<ProductFilteredDTO> ProductFiltereds { get; set; }
        public IEnumerable<CategoryFilterDTO> CategoryFilters { get; set; }
        public IEnumerable<ColorFilterDTO> ColorFilters { get; set; }

    }
}
