﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {

        public string ID { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public ProductCategory()
        {
            this.ID = Guid.NewGuid().ToString();
        }

    }
}
