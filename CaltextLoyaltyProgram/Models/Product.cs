using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaltextLoyaltyProgram.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public double UnitPrice { get; set; }
    }
}
