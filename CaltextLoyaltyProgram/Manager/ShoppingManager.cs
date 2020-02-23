using CaltextLoyaltyProgram.Data;
using CaltextLoyaltyProgram.DTOModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaltextLoyaltyProgram.Manager
{
    public class ShoppingManager : IShoppingManager
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<ShoppingManager> _logger;
        
        public ShoppingManager(ShoppingContext context, ILogger<ShoppingManager> logger)
        {
            _context = context;
            _logger = logger;
        }
        public ShoppingListReward GetShoppingReward(ShoppingList input)
        {
            if (ValidateInput(input))
            {
                return SetShoppingReward(input);
            }
            return null;
        }

        private double GetBasketDiscount(string productId, DateTime purchasedDate, double unitPrice, int numberOfItems)
        {
            var productDiscount = _context.ProductDiscount;
            
            
            var discountIds = _context.ProductDiscount.Where(x => x.ProdId == productId).Select(x => x.DiscountId).ToList(); 

            var productRecord = _context.ProductDiscount.Find(productId);

            //  List<string> promotionIds = productRecord.ProductDiscountPromotions.Select(x => x.DiscountPromotionId).ToList();


            var orderByDescendingResult = from s in _context.Discounts
                                          orderby s.Discount descending,
                                          s.StartDate,
                                          s.EndDate descending
                                          select s;
            var discountRecord = orderByDescendingResult
                .Where( x => (x.StartDate <= purchasedDate && x.EndDate >= purchasedDate ) && discountIds.Contains(x.DiscountPromotionId)).FirstOrDefault();

            double discount = 0;

            if (discountRecord != null)
            {
                discount = discountRecord.Discount * numberOfItems * unitPrice;
            }
            return discount;
        }

        private double GetBasketPoints(string productId, DateTime purchasedDate, double unitPrice, int numberOfItems)
        {
            string anyCategory = "any";
            var productCategory = _context.Products.Find(productId).Category;
            var orderByDescendingResult = from s in _context.Points
                                          orderby s.ForEachDollar descending,
                                          s.StartDate,
                                          s.EndDate descending
                                          select s;

            var pointsRecord = orderByDescendingResult
                .Where(x => (x.Category == productCategory || x.Category.ToLower() == anyCategory ) && purchasedDate >= x.StartDate && purchasedDate <= x.EndDate).SingleOrDefault();

            var points = pointsRecord.ForEachDollar * numberOfItems * unitPrice;
            return points;
        }

        private ShoppingListReward SetShoppingReward(ShoppingList input)
        {
            double totalDiscount = 0;
            double totalPoints = 0;
            double totalAmount = 0;
            var purchasedDate = DateTime.Parse(input.TransactionDate);
            ShoppingListReward shoppingListReward;


            foreach (var basketItem in input.Basket)
            {
                totalDiscount += GetBasketDiscount(basketItem.ProductId, purchasedDate, double.Parse(basketItem.UnitPrice), Convert.ToInt32(basketItem.Quantity) );
                totalPoints += GetBasketPoints(basketItem.ProductId, purchasedDate, double.Parse(basketItem.UnitPrice), Convert.ToInt32(basketItem.Quantity));
                totalAmount += Int32.Parse(basketItem.Quantity) * double.Parse(basketItem.UnitPrice);  
            }
            shoppingListReward = new ShoppingListReward()
            {
                CustomerId = input.CustomerId,
                LoyaltyCard = input.LoyaltyCard,
                TransactionDate = input.TransactionDate,
                DiscountApplied = totalDiscount.ToString(),
                GrandTotal = totalAmount.ToString(),
                PointsEarned = totalPoints.ToString(),
                TotalAmount = (totalAmount - totalDiscount).ToString(),

            };
            return shoppingListReward;
        }

        private bool ValidateInput(ShoppingList input)
        {
            if (input == null || input.Basket == null)
                return false;
            return true;
        }
    }
}
