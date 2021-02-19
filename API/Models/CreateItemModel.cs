using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class CreateItemModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int SupplierID { get; set; }
    }
}