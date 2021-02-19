using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("Tb_M_Supplier")]
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Please enter more than 5 char")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "You're allowed lala")]
        public string SupplierName { get; set; }

    }
}