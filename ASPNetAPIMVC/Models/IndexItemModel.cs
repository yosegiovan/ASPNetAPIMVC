using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetAPIMVC.Models
{
    public class IndexItemModel
    {
        public Item item_details { get; set; }
        public Supplier supplier_details { get; set; }
    }
}