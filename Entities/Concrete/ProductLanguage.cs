﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductLanguage
    {
        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSubTitle { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
        public string SeoUrl { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
