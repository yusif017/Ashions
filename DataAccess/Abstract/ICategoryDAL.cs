using Core.DataAccess;
using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace DataAccess.Abstract
{

    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        List<CategoryHomeListDTO> GetAllCategoriesLanguages(string? lanCode = "az-Az");
        IResultData<List<CategoryFeaturedDTO>> GetFeaturedCategory(string langCode);
        Task<bool> AddCategoryByLanguages(CategoryAddDTO categoyAddDTO);
        Task<IResultData<List<CategoryAdminListDTO>>> GetAdminAllCategoriesLanguages(string? lanCode = "az-Az");
        Task<bool> EditCategoryByLanguages(CategoryUpdateDTO categoyUpdateDTO);
        IEnumerable<CategoryFilterDTO> GetCategoryFilters(string langCode);
        CategoryUpdateDTO GetUpdateCategoruById(int id);
       

    }
}
