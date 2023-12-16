using Core.DataAccess.SQLServer.EntityFramework;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using Core.Utilities.SeoUrlHelper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDAL : EFRepositoryBase<Product, AppDbContext>, IProductDAL
    {
        public IResult AddProduct(string userId, ProductAddDTO productAdd)
        {
            try
            {
                using AppDbContext context = new();

                List<Picture> pictureList = new();

                for (int i = 0; i < productAdd.PhotoUrls.Count; i++)
                {
                    pictureList.Add(new Picture { PhotoUrl = productAdd.PhotoUrls[i] });
                }

                Product product = new()
                {
                    CategoryId = productAdd.CategoryId,
                    PraductOxsarId = productAdd.PraductOxsarId,
                    ProductColorId = productAdd.ProductColorId,
                    Price = productAdd.Price,
                    Quantity = productAdd.Quantity,
                    Discount = productAdd.Discount,
                    IsFeatured = productAdd.IsFeatured,
                    PraductSize = productAdd.PraductSize,
                    ProductKargo = productAdd.ProductKargo,
                    KargoPrice = productAdd.KargoPrice,
                    Pictures = pictureList,
                    UserId = userId,


                };

                context.Products.Add(product);
                context.SaveChanges();

                for (int i = 0; i < productAdd.ProductTitle.Count; i++)
                {
                    ProductLanguage productLanguage = new()
                    {
                        ProductId = product.Id,
                        ProductTitle = productAdd.ProductTitle[i],
                        ProductSubTitle = productAdd.ProductSubTitle[i],
                        Description = productAdd.Description[i],
                        SeoUrl = productAdd.ProductTitle[i].ReplaceInvalidChars(),
                        LangCode = i == 0 ? "az-Az" : i == 1 ? "ru-Ru" : "en-Us"
                    };
                    context.ProductLanguages.Add(productLanguage);
                }
                context.SaveChanges();
                return new SuccessResult("Product added successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }


        }

        public IResultData<List<ProductHomeListDTO>> GetProductBy(string? lanCode = "Az")
        {
            try
            {
                using AppDbContext context = new();

                var products = context.Products
                    .Include(x => x.ProductLanguages)
                    .Include(x => x.Pictures)
                    .Include(x => x.Category)
                    .ThenInclude(x => x.CategoryLanguages)
                    .Select(x => new ProductHomeListDTO
                    {
                        ProductName = x.ProductLanguages.FirstOrDefault(x => x.LangCode == lanCode).ProductTitle,
                        Id = x.Id,
                        Price = x.Price,
                        Discount = x.Discount,
                        SeoUrl = x.ProductLanguages.FirstOrDefault(x => x.LangCode == lanCode).SeoUrl,
                        CategoryName = x.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == lanCode).CategoryName,
                        PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                    }).ToList();

                return new SuccessDataResult<List<ProductHomeListDTO>>(products, "Products were delivered successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductHomeListDTO>>(ex.Message);
            }
        }

        public IResultData<List<PraductIsFicertDTO>> GetProductIsficeritBy(string? lanCode = "Az")
        {
            try
            {
                using AppDbContext context = new();

                var products = context.Products.Where(x=>x.IsFeatured==true)
                    .Include(x => x.ProductLanguages)
                    .Include(x => x.Pictures)
                  
                    .Select(x => new PraductIsFicertDTO
                    {
                        ProductName = x.ProductLanguages.FirstOrDefault(x => x.LangCode == lanCode).ProductTitle,
                        Id = x.Id,
                        Price = x.Price,
                        IsFeatured = x.IsFeatured,
                        SeoUrl = x.ProductLanguages.FirstOrDefault(x => x.LangCode == lanCode).SeoUrl,
                        PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                    }).ToList();

                return new SuccessDataResult<List<PraductIsFicertDTO>>(products, "Products were delivered successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<PraductIsFicertDTO>>(ex.Message);
            }
        }

        public IResultData<List<ProductAdminListDTO>> GetProductByUser(string userId, string? lanCode = "Az")
        {
            try
            {
                using AppDbContext context = new();

                var products = context.Products
                    .Include(x => x.ProductLanguages)
                    .Include(x => x.Pictures)
                    .Include(x => x.Category)
                    .ThenInclude(x => x.CategoryLanguages)
                    .Select(x => new ProductAdminListDTO
                    {
                        ProductName = x.ProductLanguages.FirstOrDefault(x => x.LangCode == lanCode).ProductTitle,
                        Id = x.Id,
                        Price = x.Price,
                        Discount = x.Discount,
                        Quantity = x.Quantity,
                        PraductSize = x.PraductSize,
                        ProductKargo = x.ProductKargo,
                        KargoPrice = x.KargoPrice,
                        CategoryName = x.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == lanCode).CategoryName,
                        ColorName = x.ProductColor.ColorLanguages.FirstOrDefault(x => x.LangCode == lanCode).ColorName,
                        PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                    }).ToList();

                return new SuccessDataResult<List<ProductAdminListDTO>>(products, "Products were delivered successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductAdminListDTO>>(ex.Message);
            }
        }

        public ProductDetailDTO GetProductDetail(int id, string langCode)
        {
            using AppDbContext context = new();

            var result = context.Products
                .Select(x => new ProductDetailDTO
                {
                    Id = x.Id,
                    ProductTitle = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductTitle,
                    ProductSubTitle = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductSubTitle,
                    Description = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).Description,
                    Price = x.Price,
                    ColorName = x.ProductColor.ColorLanguages.FirstOrDefault(x => x.LangCode == langCode).ColorName,
                    PraductSize = x.PraductSize,
                    ProductKargo = x.ProductKargo,
                    KargoPrice = x.KargoPrice,
                    Discount = x.Discount,
                    Quantity = x.Quantity,
                    PhotoUrls = x.Pictures.Where(x => x.ProductId == id).Select(x => x.PhotoUrl).ToList(),
                }).FirstOrDefault(x => x.Id == id);

            return result;
        }

        public List<UserCartDTO> GetUserCart(List<int> ids, string langCode)
        {
            using AppDbContext context = new();

            var result = context.Products
                .Where(x => ids.Contains(x.Id))
                .Include(x => x.ProductLanguages)
                .Include(x => x.Pictures)
                .Select(x => new UserCartDTO
                {
                    Id = x.Id,
                    ProductName = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductTitle,
                    PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl,
                    Price = x.Price
                }).ToList();

            return result;
        }
        
        public int GetProductCountByCategory(double take, List<int> categoryIds)
        {

            using var context = new AppDbContext();
            var result = context.Products
                .Where(x => categoryIds.Any() == false ? null == null : categoryIds.Contains(x.CategoryId)).Count();
            return result;
        }

        public int GetProductCountByColor(double take, List<int> colorIds)
        {

            using var context = new AppDbContext();
            var result = context.Products
                .Where(x => colorIds.Any() == false ? null == null : colorIds.Contains(x.ProductColorId)).Count();
            return result;
        }
        
        public IEnumerable<ProductFilteredDTO> GetProductFiltered(List<int> categoryIds, List<int> colorIds, string langCode, int minPrice, int maxPrice, int pageNo, int take)
        {
            using AppDbContext context = new();

            int next = (pageNo - 1) * take;

            var result = context.Products
                .Include(x => x.ProductLanguages)
                .Include(x => x.Pictures)
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice && (categoryIds.Any() ? categoryIds.Contains(x.CategoryId) : null == null) && (colorIds.Any() ? colorIds.Contains(x.ProductColorId) : null == null))
                .Select(x => new ProductFilteredDTO
                {
                    Id = x.Id,
                    SeoUrl = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                    ProductTitle = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductTitle,
                    Price = x.Price,
                    Discount = x.Discount,
                    PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                }).Skip(next).Take(take).ToList();

            return result;
        }

        public IResultData<List<ProductDetailCategoryDTO>> ProductDetailRecent(int id, string langCode)
        {
            using var context = new AppDbContext();

            try
            {
                var findProduct = context.Products.FirstOrDefault(x => x.Id == id).CategoryId;
                var result = context.Products.Where(c => c.CategoryId == findProduct)
                    .Select(x => new ProductDetailCategoryDTO
                    {
                        Id = x.Id,
                        ProductTitle = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductTitle,
                        Description = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).Description,
                        Price = x.Price,
                        Discount = x.Discount,
                        PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                    }).ToList();

                return new SuccessDataResult<List<ProductDetailCategoryDTO>>(result, "Products were delivered successfully");


            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<ProductDetailCategoryDTO>>(ex.Message);
            }

        }

        public IResultData<List<ProductDetailOxsarDTO>> ProductDetailOxsar(int id)
        {
            using var context = new AppDbContext();

            try
            {
                var findProduct = context.Products.FirstOrDefault(x => x.Id == id).PraductOxsarId;
                var result = context.Products.Where(c => c.PraductOxsarId == findProduct)
                    .Select(x => new ProductDetailOxsarDTO
                    {
                        Id = x.Id,
                        PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                    }).ToList();

                return new SuccessDataResult<List<ProductDetailOxsarDTO>>(result, "Products were delivered successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductDetailOxsarDTO>>(ex.Message);
            }
        }
        public ProductUpdateDTO GetUpdateProductById(int id)
        {

            using var context = new AppDbContext();

            List<ProductUpdateLanguageDTO> lg = new();
            var result = context.Products.Include(a => a.ProductLanguages).Include(p => p.Pictures).FirstOrDefault(x => x.Id == id);


            if (result == null)
            {

                return null;
            }

            ProductUpdateDTO UpdateDTO = new()
            {

                Id = result.Id,
                Lang = lg,
                CategoryId = result.CategoryId,
                IsFeatured = result.IsFeatured,
                KargoPrice = result.KargoPrice,
                PraductOxsarId = result.PraductOxsarId,
                PraductSize = result.PraductSize,
                Price = result.Price,
                Discount = result.Discount,
                ProductColorId = result.ProductColorId,
                ProductKargo = result.ProductKargo,
                Quantity = result.Quantity,
                

                PhotoUrls = result.Pictures.Select(p => p.PhotoUrl).ToList(),

            };

            for (int i = 0; i < result.ProductLanguages.Count; i++)
            {
                ProductUpdateLanguageDTO languageDTO = new()
                {
                    LangCode = i == 0 ? "az-Az" : i == 1 ? "ru-Ru" : "en-Us",
                    ProductSubTitle = result.ProductLanguages[i].ProductSubTitle,
                    Description = result.ProductLanguages[i].Description,
                    ProductTitle = result.ProductLanguages[i].ProductTitle,
                };
                lg.Add(languageDTO);
            }

            return UpdateDTO;
        }
        public async Task<bool> EditPraductByLanguages(ProductUpdateDTO updateDTO)
        {
            try
            {
                using var context = new AppDbContext();
               
                List<ProductLanguage> Languages = new();

                List<Picture> pictureList = new();

                var find = context.Products.Include(x => x.ProductLanguages).Include(x=>x.Pictures).FirstOrDefault(c => c.Id == updateDTO.Id);

                if (updateDTO.PhotoUrls.Count > 0)
                {
                  

                    // Yeni fotoğrafları eklemek için bir döngü kullanabilirsiniz
                    for (int i = 0; i < updateDTO.PhotoUrls.Count; i++)
                    {
                       
                        pictureList.Add(new Picture { PhotoUrl = updateDTO.PhotoUrls[i] });
                    }
                  
                }

                find.IsFeatured = updateDTO.IsFeatured;
                find.ProductLanguages = Languages;
                find.UpdatedDate = DateTime.Now;
                find.CategoryId = updateDTO.CategoryId;
                find.PraductOxsarId= updateDTO.PraductOxsarId;
                find.ProductColorId = updateDTO.ProductColorId;
                find.ProductKargo = updateDTO.ProductKargo;
                find.PraductSize = updateDTO.PraductSize;
                find.Quantity= updateDTO.Quantity;
                find.Price = updateDTO.Price;
                find.Discount = updateDTO.Discount;
                find.Pictures = pictureList;



                for (int i = 0; i < updateDTO.Lang.Count; i++)
                {
                    ProductLanguage language = new()
                    {

                        ProductTitle = updateDTO.Lang[i].ProductTitle,
                        ProductSubTitle = updateDTO.Lang[i].ProductSubTitle,
                        Description = updateDTO.Lang[i].Description,
                        LangCode = i == 0 ? "az-Az" : i == 1 ? "ru-Ru" : "en-Us",
                        SeoUrl = updateDTO.Lang[i].ProductTitle.ReplaceInvalidChars(),
                        ProductId = find.Id,
                       

                    };
                    Languages.Add(language);
                }
                context.Products.Update(find);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public IResultData<List<ProductSearchDTO>> GetSearch(string langCode, string title)
        {
           
                using AppDbContext context = new();
                var result = context.Set<ProductSearchDTO>().FromSqlInterpolated($"EXEC GetSea @LangCode = {langCode}, @TitleFilter={title} ").ToList();
                return new SuccessDataResult<List<ProductSearchDTO>>(result);
            
        }
    }
}

