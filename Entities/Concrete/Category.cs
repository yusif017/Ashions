using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : BaseEntitiy
    {
        public bool IsFeatured { get; set; }
        public string PhotoUrl { get; set; }
        public int ProductCaunt { get; set; }
        public List<CategoryLanguage> CategoryLanguages { get; set; }
        
    }
}
