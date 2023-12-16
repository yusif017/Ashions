using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ColorLanguage
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string SeoUrl { get; set; }
        public string LangCode { get; set; }
        public int ProductColorId { get; set; }
        public ProductColor ProductColor  { get; set; }
    }
}
