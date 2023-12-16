using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductColor : BaseEntitiy
    {
        public bool IsFeatured { get; set; }
        public List<ColorLanguage> ColorLanguages { get; set; }
        
    }
}
