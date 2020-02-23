using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaltextLoyaltyProgram.DTOModels
{
    public class ShoppingListReward
    {
        public string CustomerId { get; set; }
        public string LoyaltyCard { get; set; }
        public string TransactionDate { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountApplied { get; set; }
        public string GrandTotal { get; set; }
        public string PointsEarned { get; set; }
    }
}
