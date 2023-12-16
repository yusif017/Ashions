using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IResult AddCategory(CategoryAddDTO category);
        IResult DeleteCategory(Category category);
        IResult UpdateCategory(CategoryUpdateDTO category);
        CategoryUpdateDTO GetCategoryById(int id);
        IResultData<List<CategoryHomeListDTO>> GetAllCategories(string langCode);
        IResultData<List<CategoryFeaturedDTO>> GetAllFeaturedCategory(string langCode);
        IResultData<IEnumerable<CategoryFilterDTO>> GetAllFilterCategories(string langCode);
        Task<IResultData<List<CategoryAdminListDTO>>> GetAllAdminCategories(string langCode);
        

    }
}
