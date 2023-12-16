using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ColorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;
using static Entities.DTOs.ColorDTOs.ColorDTO;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDAL _colorDAL;

        public ColorManager(IColorDAL colorDAL)
        {
            _colorDAL = colorDAL;
        }

        public IResult AddColor(ColorAddDTO color)
        {
            try
            {
                _colorDAL.AddColorByLanguages(color);
                return new SuccessResult("Coloror Added Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public IResult DeleteColor(ProductColor color)
        {
            try
            {
                _colorDAL.Delete(color);
                return new SuccessResult("color Delete Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IResultData<List<ColorAdminListDTO>>> GetAllAdminColor(string langCode)
        {
            var result = await _colorDAL.GetAdminAllColorLanguages(langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<ColorAdminListDTO>>(result.Data);
            }
            return new ErrorDataResult<List<ColorAdminListDTO>>(result.Message);
        }

        public IResultData<List<ColorHomeListDTO>> GetAllColor(string langCode)
        {
            var result = _colorDAL.GetAllColorLanguages(langCode);
            return new SuccessDataResult<List<ColorHomeListDTO>>(result, "All Color");
        }

        public IResultData<List<ColorFeaturedDTO>> GetAllFeaturedColor(string langCode)
        {
            var result = _colorDAL.GetFeaturedColor(langCode);
            return new SuccessDataResult<List<ColorFeaturedDTO>>(result.Data);
        }

        public IResultData<IEnumerable<ColorFilterDTO>> GetAllFilterColor(string langCode)
        {
            var result = _colorDAL.GetColorFilters(langCode);
            return new SuccessDataResult<IEnumerable<ColorFilterDTO>>(result);
        }

        public ColorUpdateDTO GetColorById(int id)
        {
            return _colorDAL.GetUpdateColorById(id);
        }

        public IResult UpdateColor(ColorUpdateDTO color)
        {
            try
            {
                _colorDAL.EditColorByLanguages(color);
                return new SuccessResult("Color Added Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
