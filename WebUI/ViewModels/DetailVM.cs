using Entities.DTOs.ProductDTOs;

namespace WebUI.ViewModels
{
    public class DetailVM
    {
        public List<ProductDetailCategoryDTO> productDetailCategoryDTOs { get; set; }
        public ProductDetailDTO productDetailDTOs { get; set; }
        public List<ProductDetailOxsarDTO> productDetailOxsarDTOs  { get; set; }
    }
}
