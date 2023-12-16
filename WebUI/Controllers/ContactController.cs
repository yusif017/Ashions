using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ContactDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactAddDTO contactAddDTO)
        {

            _contactService.AddContent(contactAddDTO);
            return View();
        }


    }
}
