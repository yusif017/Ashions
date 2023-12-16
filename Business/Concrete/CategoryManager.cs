using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public IResult AddCategory(CategoryAddDTO category)
        {
            try
            {
                _categoryDAL.AddCategoryByLanguages(category);
                return new SuccessResult("Blog Added Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public IResultData<IEnumerable<CategoryFilterDTO>> GetAllFilterCategories(string langCode)
        {
            var result = _categoryDAL.GetCategoryFilters(langCode);
            return new SuccessDataResult<IEnumerable<CategoryFilterDTO>>(result);
        }




        public IResult DeleteCategory(Category category)
        {
           
            try
            {
                _categoryDAL.Delete(category);
                return new SuccessResult("Category Delete Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IResultData<List<CategoryAdminListDTO>>> GetAllAdminCategories(string langCode)
        {
            var result = await _categoryDAL.GetAdminAllCategoriesLanguages(langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<CategoryAdminListDTO>>(result.Data);
            }
            return new ErrorDataResult<List<CategoryAdminListDTO>>(result.Message);
        }

        public IResultData<List<CategoryHomeListDTO>> GetAllCategories(string langCode)
        {
            var result = _categoryDAL.GetAllCategoriesLanguages(langCode);
            return new SuccessDataResult<List<CategoryHomeListDTO>>(result, "All Categories");
        }
        public IResultData<List<CategoryFeaturedDTO>> GetAllFeaturedCategory(string langCode)
        {
            var result = _categoryDAL.GetFeaturedCategory(langCode);
            return new SuccessDataResult<List<CategoryFeaturedDTO>>(result.Data);
        }

        public CategoryUpdateDTO GetCategoryById(int id)
        {       
            return _categoryDAL.GetUpdateCategoruById(id);
        }


        public IResult UpdateCategory(CategoryUpdateDTO category)
        {

            try
            {
                _categoryDAL.EditCategoryByLanguages(category);
                return new SuccessResult("Blog Added Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
