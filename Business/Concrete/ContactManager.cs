using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.DTOs.ContactDTOs;


namespace Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDAL _contactDAL;

        public ContactManager(IContactDAL contactDAL)
        {
            _contactDAL = contactDAL;
        }

        public IResult AddContent(ContactAddDTO contact)
        {
            try
            {
                _contactDAL.AddContact(contact);
                return new SuccessResult("Contact Added Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public  IResultData<List<ContactListDTO>> GetAllContent()
        {
            var result = _contactDAL.GetAllContact();
            return new SuccessDataResult<List<ContactListDTO>>(result, "All Categories");
        }
        public IResultData<ContactDetailDTO> GetContactById(int id)
        {
            var result = _contactDAL.GetContactDetail(id);

            return new SuccessDataResult<ContactDetailDTO>(result);
        }
    }

      
    }

