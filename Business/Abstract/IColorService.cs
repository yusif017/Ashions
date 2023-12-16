using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ColorDTOs;
using static Entities.DTOs.ColorDTOs.ColorDTO;

namespace Business.Abstract
{
    public interface IColorService
    {
        IResult AddColor(ColorAddDTO color);
        IResult DeleteColor(ProductColor color);
        IResult UpdateColor(ColorUpdateDTO color);
        ColorUpdateDTO GetColorById(int id);
        IResultData<List<ColorFeaturedDTO>> GetAllFeaturedColor(string langCode);
        IResultData<IEnumerable<ColorFilterDTO>> GetAllFilterColor(string langCode);
        IResultData<List<ColorHomeListDTO>> GetAllColor(string langCode);
        Task<IResultData<List<ColorAdminListDTO>>> GetAllAdminColor(string langCode);
    }
}
