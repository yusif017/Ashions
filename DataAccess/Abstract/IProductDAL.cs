using Core.DataAccess;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Entities.Concrete;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace DataAccess.Abstract
{
    public interface IProductDAL : IRepositoryBase<Product>
    {
        IResultData<List<ProductAdminListDTO>> GetProductByUser(string userId, string? lanCode = "Az");
        IResultData<List<ProductHomeListDTO>> GetProductBy(string? lanCode = "Az");
        IResult AddProduct(string userId, ProductAddDTO productAdd);
        ProductDetailDTO GetProductDetail(int id, string langCode);
        List<UserCartDTO> GetUserCart(List<int> ids, string langCode);
        int GetProductCountByCategory(double take, List<int> categoryIds);
        int GetProductCountByColor(double take, List<int> colorIds);
        IEnumerable<ProductFilteredDTO> GetProductFiltered(List<int> categoryIds, List<int> colorIds, string langCode, int minPrice, int maxPrice, int pageNo, int take);
        IResultData<List<PraductIsFicertDTO>> GetProductIsficeritBy(string? lanCode = "Az");
        IResultData<List<ProductSearchDTO>> GetSearch(string langCode, string title);
        Task<bool> EditPraductByLanguages(ProductUpdateDTO updateDTO);
        ProductUpdateDTO GetUpdateProductById(int id);
        IResultData<List<ProductDetailCategoryDTO>> ProductDetailRecent(int id , string langCode);
        IResultData<List<ProductDetailOxsarDTO>> ProductDetailOxsar(int id);

    }
}
