using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("Tb_M_Item")]
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

    }
}