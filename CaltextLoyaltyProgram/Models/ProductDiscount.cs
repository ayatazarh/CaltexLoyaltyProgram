using System.ComponentModel.DataAnnotations;

namespace CaltextLoyaltyProgram.Models
{
    public class ProductDiscount
    {
        [Key]
        public string ProdDiscountId { get; set; }
        public string ProdId { get; set; }
        public string DiscountId { get; set; }
    }
}
