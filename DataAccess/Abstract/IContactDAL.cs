

using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.ContactDTOs;

namespace DataAccess.Abstract
{
    public interface IContactDAL:IRepositoryBase<Contact>
    {
        List<ContactListDTO> GetAllContact();
        Task<bool> AddContact(ContactAddDTO contactAddDTO);
        ContactDetailDTO GetContactDetail(int id);

    }
}
