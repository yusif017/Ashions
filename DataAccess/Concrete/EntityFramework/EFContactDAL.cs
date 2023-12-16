using Core.DataAccess.SQLServer.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ContactDTOs;
namespace DataAccess.Concrete.EntityFramework
{
    public class EFContactDAL : EFRepositoryBase<Contact, AppDbContext>, IContactDAL
    {
        public async Task<bool> AddContact(ContactAddDTO contactAddDTO)
        {

            try
            {
                using var context = new AppDbContext();


                Contact contact = new()
                {
                    Content = contactAddDTO.Content,
                    Name = contactAddDTO.Name,
                    PublishData = contactAddDTO.PublishData,
                    Email = contactAddDTO.Email,

                };

                await context.Contacts.AddAsync(contact);
                await context.SaveChangesAsync();


                return true;
            }
            catch (Exception)
            {
                return false;
            }



        }
        
        public ContactDetailDTO GetContactDetail(int id)
        {
            using AppDbContext context = new();

            var result = context.Contacts
                .Select(x => new ContactDetailDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Content = x.Content,
                    Email = x.Email,

                }).FirstOrDefault(x => x.Id == id);

            return result;
        }

        public List<ContactListDTO> GetAllContact()
        {
            using var context = new AppDbContext();
            return context.Contacts

                .Select(x => new ContactListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Content = x.Content,
                    Email = x.Email,
                    PublishData = x.PublishData,



                }).ToList();
        }
    }
}
