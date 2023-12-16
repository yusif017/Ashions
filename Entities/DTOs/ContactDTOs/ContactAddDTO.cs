using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ContactDTOs
{
    public class ContactAddDTO
    {
       
        public string Content { get; set; }
     
        public string Name { get; set; }
      
        public string Email { get; set; }
        public DateTime PublishData { get; set; }


    }
}
