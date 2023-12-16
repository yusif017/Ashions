using Core.Utilities.Abstract;
using Entities.DTOs.ContactDTOs;


namespace Business.Abstract
{
    public interface IContactService
    {
        IResult AddContent(ContactAddDTO contact);
        IResultData<List<ContactListDTO>> GetAllContent();
        IResultData<ContactDetailDTO> GetContactById(int id);

    }
}
