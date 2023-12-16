using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Reflection.Metadata;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public IResult AddProductAdmin(string userId, ProductAddDTO productAdd)
        {
            var result = _productDAL.AddProduct(userId, productAdd);
            if (result.Success)
            {
                return new SuccessResult(result.Message);
            }
            return new ErrorResult(result.Message);
        }
       
        public IResultData<int> GetProductQuantityById(int productId)
        {
            var productCount = _productDAL.Get(x => x.Id == productId).Quantity;
            return new SuccessDataResult<int>(productCount);
        }
        
        public IResultData<ProductDetailDTO> GetProductById(int id, string langCode)
        {
            var result = _productDAL.GetProductDetail(id, langCode);

            return new SuccessDataResult<ProductDetailDTO>(result);
        }
        
        public IResultData<int> GetProductCount(double take, List<int> categoryIds, List<int> colorIds)
        {
            double categoryCount = _productDAL.GetProductCountByCategory(take, categoryIds);
            double colorCount = _productDAL.GetProductCountByColor(take, colorIds);

            double totalResult = (categoryCount + colorCount) / take;
            int productCountResult = (int)Math.Ceiling(totalResult);

            return new SuccessDataResult<int>(productCountResult);
        }
        
        public IResultData<List<ProductHomeListDTO>> GetHomeProducts(string langCode)
        {
            var result = _productDAL.GetProductBy(langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<ProductHomeListDTO>>(result.Data);
            }
            return new ErrorDataResult<List<ProductHomeListDTO>>(result.Message);
        }

        public IResultData<List<ProductAdminListDTO>> GetDashboardProducts(string userId, string langCode)
        {
            var result = _productDAL.GetProductByUser(userId, langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<ProductAdminListDTO>>(result.Data);
            }
            return new ErrorDataResult<List<ProductAdminListDTO>>(result.Message);
        }
        public IResult DeleteProduct(Product product)
        {
            try
            {
                _productDAL.Delete(product);
                return new SuccessResult("Product Delete Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
        public IResultData<IEnumerable<ProductFilteredDTO>> GetAllFilteredProducts(List<int> categoryIds, List<int> colorIds, string langCode, int minPrice, int maxPrice, int pageNo, int take)
        {
            var result = _productDAL.GetProductFiltered(categoryIds,colorIds, langCode, minPrice, maxPrice, pageNo, take);
            return new SuccessDataResult<IEnumerable<ProductFilteredDTO>>(result);
        }
        public IResultData<List<UserCartDTO>> GetProductForCart(List<int> ids, string langCode, List<int> quantity)
        {
            var result = _productDAL.GetUserCart(ids, langCode);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Quantity = quantity[i];
            }

            return new SuccessDataResult<List<UserCartDTO>>(result);
        }

        public IResultData<List<ProductDetailCategoryDTO>> GetProductDetilCategoryPraduct(int id, string langCode)
        {
            var result = _productDAL.ProductDetailRecent(id,langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<ProductDetailCategoryDTO>>(result.Data);
            }
            return new ErrorDataResult<List<ProductDetailCategoryDTO>>(result.Message);
        }

        public IResultData<List<ProductDetailOxsarDTO>> GetProductDetilOxsarPraduct(int id)
        {
            var result = _productDAL.ProductDetailOxsar(id);
            if (result.Success)
            {
                return new SuccessDataResult<List<ProductDetailOxsarDTO>>(result.Data);
            }
            return new ErrorDataResult<List<ProductDetailOxsarDTO>>(result.Message);
        }

        public IResultData<List<PraductIsFicertDTO>> GetIsFeacredProducts(string langCode)
        {
            var result = _productDAL.GetProductIsficeritBy(langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<PraductIsFicertDTO>>(result.Data);
            }
            return new ErrorDataResult<List<PraductIsFicertDTO>>(result.Message);
        }

        public IResult UpdateProduct(ProductUpdateDTO updateDTO)
        {
            try
            {
                _productDAL.EditPraductByLanguages(updateDTO);

                return new SuccessResult("Product Updata Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public ProductUpdateDTO GetProductById(int id)
        {
            return _productDAL.GetUpdateProductById(id);
        }

        public IResultData<List<ProductSearchDTO>> GetSearchProducts(string langCode, string title)
        {

            
                var result = _productDAL.GetSearch(langCode ,title);

                return new SuccessDataResult<List<ProductSearchDTO>>(result.Data);
            

        }
    }
}
