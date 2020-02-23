using CaltextLoyaltyProgram.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaltextLoyaltyProgram.Manager
{
    public interface IShoppingManager
    {
        ShoppingListReward GetShoppingReward(ShoppingList input);
    }
}
