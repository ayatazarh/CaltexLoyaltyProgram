using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaltextLoyaltyProgram.DTOModels
{
    public class ShoppingList
    {
        public int ShoppingListId { get; set; }
        public string CustomerId { get; set; }
        public string LoyaltyCard { get; set; }
        public string TransactionDate { get; set; }
        public List<Basket> Basket { get; set; }
    }
}
