using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ContactDTOs
{
    public class ContactListDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public DateTime PublishData { get; set; }


    }
}
