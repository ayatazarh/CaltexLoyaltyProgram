using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaltextLoyaltyProgram.DTOModels
{
   public class Basket
    {
        public int BasketId { get; set; }
        public string ProductId { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
    }
}
