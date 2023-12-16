

using Core.DataAccess.SQLServer.EntityFramework;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using Core.Utilities.SeoUrlHelper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ColorDTOs;
using Microsoft.EntityFrameworkCore;
using static Entities.DTOs.ColorDTOs.ColorDTO;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFColorDAL : EFRepositoryBase<ProductColor, AppDbContext>, IColorDAL
    {
        
        public async Task<bool> AddColorByLanguages(ColorAddDTO colorAddDTO)
        {
            try
            {
                using AppDbContext context = new();

                ProductColor productColor = new()
                {
                    IsFeatured = true,

                };

                await context.ProductColors.AddAsync(productColor);
                await context.SaveChangesAsync();

                for (int i = 0; i < colorAddDTO.ColorName.Count; i++)
                {
                    ColorLanguage colorLanguage = new()
                    {
                        ColorName = colorAddDTO.ColorName[i],
                        ProductColorId = productColor.Id,
                        LangCode = i == 0 ? "az-Az" : i == 1 ? "en-Us" : "ru-Ru",
                        SeoUrl = colorAddDTO.ColorName[i].ReplaceInvalidChars(),
                    };

                    await context.ColorLanguages.AddAsync(colorLanguage);
                }
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public IEnumerable<ColorFilterDTO> GetColorFilters(string langCode)
        {
            using AppDbContext context = new();
            var result = context.ProductColors
                .Include(x => x.ColorLanguages)
                .Select(x => new ColorFilterDTO
                {
                    Id = x.Id,
                    ColorName = x.ColorLanguages.FirstOrDefault(x => x.LangCode == langCode).ColorName
                }).ToList();
            return result;
        }
        
        public async Task<bool> EditColorByLanguages(ColorUpdateDTO colorUpdateDTO)
        {
            try
            {
                using var context = new AppDbContext();
                var findColor = context.ProductColors.Include(x => x.ColorLanguages).FirstOrDefault(c => c.Id == colorUpdateDTO.Id);
                List<ColorLanguage> colorLanguages = new();
                findColor.IsFeatured = colorUpdateDTO.IsFeatured;
                findColor.ColorLanguages = colorLanguages;
                findColor.UpdatedDate = DateTime.Now;


                for (int i = 0; i < colorUpdateDTO.ColorLang.Count; i++)
                {
                    ColorLanguage language = new()
                    {
                        ColorName = colorUpdateDTO.ColorLang[i].Title,
                        LangCode = i == 0 ? "az-AZ" : i == 1 ? "en-US" : "ru-RU",
                        SeoUrl = colorUpdateDTO.ColorLang[i].Title.ReplaceInvalidChars(),
                        ProductColorId = findColor.Id,
                    };
                    colorLanguages.Add(language);
                }
                context.ProductColors.Update(findColor);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IResultData<List<ColorAdminListDTO>>> GetAdminAllColorLanguages(string langCode)
        {
            using AppDbContext context = new();
            try
            {
                var result = await context.ProductColors
                    .Select(x => new ColorAdminListDTO
                    {
                        ColorName = x.ColorLanguages.FirstOrDefault(x => x.LangCode == langCode).ColorName,
                        IsFeatured = x.IsFeatured,
                        Id = x.Id,

                    }).ToListAsync();

                return new SuccessDataResult<List<ColorAdminListDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ColorAdminListDTO>>(ex.Message);
            }
        }

        public IResultData<List<ColorFeaturedDTO>> GetFeaturedColor(string langCode)
        {
            using AppDbContext context = new();

            var result = context.ProductColors
                .Include(x => x.ColorLanguages)
                .ThenInclude(x => x.ProductColor)
                .Where(x => x.IsFeatured == true)
                .Select(x => new ColorFeaturedDTO(x.Id,
                x.ColorLanguages.FirstOrDefault(x => x.LangCode == langCode).ColorName,
                x.ColorLanguages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl
                )).ToList();

            return new SuccessDataResult<List<ColorFeaturedDTO>>(result);
        }
       
        public ColorUpdateDTO GetUpdateColorById(int id)
        {
            using var context = new AppDbContext();
            List<ColorUpdateLanguageDTO> lg = new();
            var result = context.ProductColors.Include(a => a.ColorLanguages).FirstOrDefault(x => x.Id == id);
            ColorUpdateDTO colorUpdateDTO = new()
            {
                Id = result.Id,
                ColorLang = lg,

            };
            for (int i = 0; i < result.ColorLanguages.Count; i++)
            {
                ColorUpdateLanguageDTO languageDTO = new()
                {
                    LanngCode = i == 0 ? "az-AZ" : i == 1 ? "en-US" : "ru-RU",
                    Title = result.ColorLanguages[i].ColorName
                };
                lg.Add(languageDTO);
            }

            return colorUpdateDTO;
        }

        public void Update(ColorUpdateDTO color)
        {
            using var context = new AppDbContext();

            ProductColor cat = new()
            {

                IsFeatured = true,
            };

            context.ProductColors.Update(cat);
            context.SaveChanges();

            for (int i = 0; i < color.ColorLang.Count; i++)
            {
                ColorLanguage Language = new()
                {
                    ColorName = color.ColorLang[i].Title,
                    ProductColorId = cat.Id,
                    LangCode = i == 0 ? "az-AZ" : i == 1 ? "en-US" : "ru-RU",
                    SeoUrl = color.ColorLang[i].Title.ReplaceInvalidChars(),
                };

                context.ColorLanguages.Update(Language);
            }
            context.SaveChanges();
        }

        public List<ColorHomeListDTO> GetAllColorLanguages(string? lanCode = "az-Az")
        {
            using var context = new AppDbContext();
            return context.ColorLanguages
                .Where(x => x.LangCode == lanCode)
                .Include(x => x.ProductColor)
                .Select(x => new ColorHomeListDTO
                {
                    Id = x.ProductColor.Id,
                    ColorName = x.ColorName,
                    SeoUrl = x.SeoUrl,

                }).ToList();
        }
    }
}
