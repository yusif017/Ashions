using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : BaseEntitiy
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal KargoPrice { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsFeatured { get; set; }
        public List<ProductLanguage> ProductLanguages { get; set; }
        public List<Picture> Pictures { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int PraductOxsarId { get; set; }
        public PraductOxsar PraductOxsar { get; set; }
        public int ProductColorId { get; set; }
        public ProductColor ProductColor { get; set; }

        public PraductSize PraductSize { get; set; }
        public ProductKargo ProductKargo { get; set; }

    }


}
