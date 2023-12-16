using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;


namespace Business.Abstract
{
    public interface IProductService
    {
    
        IResultData<List<ProductAdminListDTO>> GetDashboardProducts(string userId, string langCode);
        IResult AddProductAdmin(string userId, ProductAddDTO productAdd);
        IResult DeleteProduct(Product product);
        IResultData<ProductDetailDTO> GetProductById(int id, string langCode);
        IResultData<List<ProductDetailCategoryDTO>> GetProductDetilCategoryPraduct(int id, string langCode);
        IResultData<List<ProductDetailOxsarDTO>> GetProductDetilOxsarPraduct(int id);
        IResultData<List<ProductHomeListDTO>> GetHomeProducts(string langCode);
        IResultData<List<UserCartDTO>> GetProductForCart(List<int> ids, string langCode, List<int> quantity);
        IResultData<IEnumerable<ProductFilteredDTO>> GetAllFilteredProducts(List<int> categoryIds, List<int> colorIds, string langCode, int minPrice, int maxPrice, int pageNo, int take);
        IResultData<int> GetProductCount(double take, List<int> categoryIds, List<int> colorIds);
        IResultData<List<ProductSearchDTO>> GetSearchProducts(string langCode, string title);
        IResultData<int> GetProductQuantityById(int productId);
        IResultData<List<PraductIsFicertDTO>> GetIsFeacredProducts(string langCode);
        IResult UpdateProduct(ProductUpdateDTO updateDTO);
        ProductUpdateDTO GetProductById(int id);
    }
}
