using Core.DataAccess.SQLServer.EntityFramework;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using Core.Utilities.SeoUrlHelper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public async Task<bool> AddCategoryByLanguages(CategoryAddDTO categoryAddDTO)
        {
            try
            {
                using AppDbContext context = new();

                Category category = new()
                {
                    IsFeatured = true,
                    PhotoUrl = categoryAddDTO.PhotoUrl,

                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                for (int i = 0; i < categoryAddDTO.CategoryName.Count; i++)
                {
                    CategoryLanguage categoryLanguage = new()
                    {
                        CategoryName = categoryAddDTO.CategoryName[i],
                        CategoryId = category.Id,
                        LangCode = i == 0 ? "az-Az" : i == 1 ? "en-Us" : "ru-Ru",
                        SeoUrl = categoryAddDTO.CategoryName[i].ReplaceInvalidChars(),
                    };

                    await context.CategoryLanguages.AddAsync(categoryLanguage);
                }
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        public IEnumerable<CategoryFilterDTO> GetCategoryFilters(string langCode)
        {
            using AppDbContext context = new();
            var result = context.Categories
                .Include(x => x.CategoryLanguages)
                .Select(x => new CategoryFilterDTO
                {
                    Id = x.Id,
                    CategoryName = x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).CategoryName
                }).ToList();
            return result;
        }
        
        public async Task<bool> EditCategoryByLanguages(CategoryUpdateDTO categoyUpdateDTO)
        {
            try
            {
                using var context = new AppDbContext();
                var findCategory = context.Categories.Include(x => x.CategoryLanguages).FirstOrDefault(c => c.Id == categoyUpdateDTO.Id);
                List<CategoryLanguage> categoryLanguages = new();
                findCategory.IsFeatured = categoyUpdateDTO.IsFeatured;
                findCategory.CategoryLanguages = categoryLanguages;
                findCategory.UpdatedDate = DateTime.Now;
                findCategory.PhotoUrl = categoyUpdateDTO.PhotoUrl;

                for (int i = 0; i < categoyUpdateDTO.CategoryLang.Count; i++)
                {
                    CategoryLanguage language = new()
                    {
                        CategoryName = categoyUpdateDTO.CategoryLang[i].Title,
                        LangCode = i == 0 ? "az-AZ" : i == 1 ? "en-US" : "ru-RU",
                        SeoUrl = categoyUpdateDTO.CategoryLang[i].Title.ReplaceInvalidChars(),
                        CategoryId = findCategory.Id,
                    };
                    categoryLanguages.Add(language);
                }
                context.Categories.Update(findCategory);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IResultData<List<CategoryAdminListDTO>>> GetAdminAllCategoriesLanguages(string langCode)
        {
            using AppDbContext context = new();
            try
            {
                var result = await context.Categories
                    .Select(x => new CategoryAdminListDTO
                    {
                        CategoryName = x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                        PhotoUrl = x.PhotoUrl,

                        IsFeatured = x.IsFeatured,
                        Id = x.Id,

                    }).ToListAsync();

                return new SuccessDataResult<List<CategoryAdminListDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryAdminListDTO>>(ex.Message);
            }
        }

        public List<CategoryHomeListDTO> GetAllCategoriesLanguages(string langCode)
        {
            using var context = new AppDbContext();
            return context.CategoryLanguages
                .Where(x => x.LangCode == langCode)
                .Include(x => x.Category)
                .Select(x => new CategoryHomeListDTO
                {
                    Id = x.Category.Id,
                    CategoryName = x.CategoryName,
                    PhotoUrl = x.Category.PhotoUrl,
                    SeoUrl = x.SeoUrl,

                }).ToList();
        }
        
        public IResultData<List<CategoryFeaturedDTO>> GetFeaturedCategory(string langCode)
        {
            using AppDbContext context = new();

            var result = context.Categories
                .Include(x => x.CategoryLanguages)
                .ThenInclude(x => x.Category)
                .Where(x => x.IsFeatured == true)
                .Select(x => new CategoryFeaturedDTO(x.Id,
                x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                x.PhotoUrl,
                x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                0)).ToList();

            return new SuccessDataResult<List<CategoryFeaturedDTO>>(result);
        }
        
        public CategoryUpdateDTO GetUpdateCategoruById(int id)
        {
            using var context = new AppDbContext();
            List<CategoryUpdateLanguageDTO> lg = new();
            var result = context.Categories.Include(a => a.CategoryLanguages).FirstOrDefault(x => x.Id == id);
            CategoryUpdateDTO categoryUpdateDTO = new()
            {
                Id = result.Id,
                CategoryLang = lg,
                PhotoUrl = result.PhotoUrl,
            };
            for (int i = 0; i < result.CategoryLanguages.Count; i++)
            {
                CategoryUpdateLanguageDTO languageDTO = new()
                {
                    LanngCode = i == 0 ? "az-AZ" : i == 1 ? "en-US" : "ru-RU",
                    Title = result.CategoryLanguages[i].CategoryName
                };
                lg.Add(languageDTO);
            }

            return categoryUpdateDTO;
        }

        public void Update(CategoryUpdateDTO category)
        {
            using var context = new AppDbContext();

            Category cat = new()
            {

                IsFeatured = true,
                PhotoUrl = category.PhotoUrl,
            };

            context.Categories.Update(cat);
            context.SaveChanges();

            for (int i = 0; i < category.CategoryLang.Count; i++)
            {
                CategoryLanguage categoryLanguage = new()
                {
                    CategoryName = category.CategoryLang[i].Title,
                    CategoryId = cat.Id,
                    LangCode = i == 0 ? "az-AZ" : i == 1 ? "en-US" : "ru-RU",
                    SeoUrl = category.CategoryLang[i].Title.ReplaceInvalidChars(),
                };

                context.CategoryLanguages.Update(categoryLanguage);
            }
            context.SaveChanges();
        }
    }
}
