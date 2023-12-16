using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace WebUI.ViewModels
{
    public class HomeVM
    {
        public List<CategoryFeaturedDTO> CategoryFeaturedDTOs { get; set; }
        public List<CategoryHomeListDTO> CategoryHomeListDTO { get; set; }
        public List<ProductHomeListDTO> ProductHomeListDTO { get; set; }
        public List<PraductIsFicertDTO> PraductIsFicertDTO { get; set; }

    }
}
