using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaltextLoyaltyProgram.DTOModels;
using CaltextLoyaltyProgram.Data;
using Microsoft.Extensions.Logging;
using CaltextLoyaltyProgram.Models;
using CaltextLoyaltyProgram.Manager;

namespace CaltextLoyaltyProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingManager _shoppingManager;
        private readonly ILogger<ShoppingController> _logger;

        public ShoppingController(IShoppingManager shoppingManager, ILogger<ShoppingController> logger)
        {
            _shoppingManager = shoppingManager;
            _logger = logger;
        }

       // POST: api/Shopping
       [HttpPost]
        public ActionResult<ShoppingListReward> PostShoppingListReward(ShoppingList shoppingList)
        {
            var shoppingListReward = _shoppingManager.GetShoppingReward(shoppingList);
            if (shoppingListReward == null)
                return BadRequest();
            return CreatedAtAction(nameof(GetShoppingList),
             new { id = Guid.NewGuid() },
             shoppingListReward);

        }


        // GET: api/Shopping
        [HttpGet]
        public ActionResult<string> GetShoppingList()
        {
            return  Ok( " Loyalty Program. Post the shopping list to calculate your rewards.");
        }


        
    }
}
