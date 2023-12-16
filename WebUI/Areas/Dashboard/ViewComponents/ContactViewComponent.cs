
using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.ViewComponents
{
  
        public class ContactViewComponent : ViewComponent
        {

        private readonly IContactService _contactService;

        public ContactViewComponent(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Categoriy componeti
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
            {
            var result = _contactService.GetAllContent();
            if (result.Success)
            {
                return View(result.Data);

            }


            return View();
            }
        }
         
}
