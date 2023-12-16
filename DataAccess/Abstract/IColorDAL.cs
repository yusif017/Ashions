using Core.DataAccess;
using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ColorDTOs;
using static Entities.DTOs.ColorDTOs.ColorDTO;

namespace DataAccess.Abstract
{

    public interface IColorDAL : IRepositoryBase<ProductColor>
    {

        IResultData<List<ColorFeaturedDTO>> GetFeaturedColor(string langCode);
        Task<bool> AddColorByLanguages(ColorAddDTO colorAddDTO);
        Task<IResultData<List<ColorAdminListDTO>>> GetAdminAllColorLanguages(string? lanCode = "az-Az");
        Task<bool> EditColorByLanguages(ColorUpdateDTO colorUpdateDTO);
        IEnumerable<ColorFilterDTO> GetColorFilters(string langCode);
        ColorUpdateDTO GetUpdateColorById(int id);
        List<ColorHomeListDTO> GetAllColorLanguages(string? lanCode = "az-Az");



    }
}
