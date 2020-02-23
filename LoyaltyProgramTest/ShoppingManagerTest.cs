using CaltextLoyaltyProgram.Data;
using CaltextLoyaltyProgram.Manager;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using CaltextLoyaltyProgram.DTOModels;
using System.Collections.Generic;
using System;

namespace LoyaltyProgramTest
{
    public class ShoppingManagerTests
    {
        IShoppingManager shoppingManager;

        Basket validBasket = new Basket()
        {
            BasketId = 1,
            ProductId = "PRD01",
            Quantity = "5",
            UnitPrice = "1.5"
        };
        ShoppingList invalidRequest = new ShoppingList()
        {
            CustomerId = "Cust1",
            LoyaltyCard = "Card01",
            ShoppingListId = 10000,
            TransactionDate = "01-Jan-2020"
        };

        ShoppingList validRequest = new ShoppingList()
        {
            CustomerId = "Cust1",
            LoyaltyCard = "Card01",
            ShoppingListId = 10000,
            TransactionDate = "01-Jan-2020",

        };


        [SetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("Data Source = shopping.db");
            connection.Open();

            validRequest.Basket = new List<Basket>();
            validRequest.Basket.Add(validBasket);

            try
            {
                var options = new DbContextOptionsBuilder<ShoppingContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                var context = new ShoppingContext(options);
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                shoppingManager = new ShoppingManager(context, new NullLogger<ShoppingManager>());

            }
            finally
            {
                connection.Close();
            }
        }

        [Test]
        public void Invalid_Input_Thrwo_Exception_Test()
        {
            
            Assert.AreEqual(null, shoppingManager.GetShoppingReward(invalidRequest));
        }

        [Test]
        public void Valid_Input_Thrwo_Exception_Test()
        {
            var response = shoppingManager.GetShoppingReward(validRequest);
            Assert.AreEqual("1.5", response.DiscountApplied);
            Assert.AreEqual("15", response.PointsEarned);
        }
    }    
}