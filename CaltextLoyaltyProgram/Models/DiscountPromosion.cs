using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaltextLoyaltyProgram.Models
{
    public class DiscountPromosion
    {
        [Key]
        public string DiscountPromotionId { get; set; }
        public string PromsionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Discount { get; set; }
       
    }
}
