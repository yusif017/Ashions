using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public DateTime PublishData { get; set; } = DateTime.Now;

    }
}
 