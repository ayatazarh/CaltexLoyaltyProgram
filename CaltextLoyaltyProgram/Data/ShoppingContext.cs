using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CaltextLoyaltyProgram.DTOModels;
using CaltextLoyaltyProgram.Models;

namespace CaltextLoyaltyProgram.Data
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<DiscountPromosion> Discounts { get; set; }
        public DbSet<PointsPromosion> Points { get; set; }
        public DbSet<ProductDiscount> ProductDiscount { get; set; }

        public ShoppingContext (DbContextOptions<ShoppingContext> options)
            : base(options)
        {
            try
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            catch (Exception e)
            {

            }
        }

        

        //Data seeding Etity framework core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Product product1 = new Product() { ProductId = "PRD01", ProductName = "Vortex 95", Category = "Fuel", UnitPrice = 1.2 };
            modelBuilder.Entity<Product>().HasData(
                product1,
                new Product() { ProductId = "PRD02", ProductName = "Vortex 98", Category = "Fuel", UnitPrice = 1.3 },
                new Product() { ProductId = "PRD03", ProductName = "Diesel", Category = "Fuel", UnitPrice = 1.1 },
                new Product() { ProductId = "PRD04", ProductName = "Twix 55g", Category = "Shop", UnitPrice = 2.3 },
                new Product() { ProductId = "PRD05", ProductName = "Mars 72g", Category = "Shop", UnitPrice = 5.1 },
                new Product() { ProductId = "PRD06", ProductName = "SNICKERS 72G", Category = "shop", UnitPrice = 3.4 },
                new Product() { ProductId = "PRD07", ProductName = "Bounty 3 63g", Category = "Shop", UnitPrice = 6.9 },
                new Product() { ProductId = "PRD08", ProductName = "Snickers 50g", Category = "Shop", UnitPrice = 4.0 }
                );
            modelBuilder.Entity<PointsPromosion>().HasData(
               new PointsPromosion() { PointsPromosionId = "PP001", PromosionName = "New Year Promo", StartDate = DateTime.Parse("1-Jan-2020"), EndDate = DateTime.Parse("30-Jan-2020"), Category="Any", ForEachDollar=2 },
               new PointsPromosion() { PointsPromosionId = "PP002", PromosionName = "FuelPromo", StartDate = DateTime.Parse("05-Feb-2020"), EndDate = DateTime.Parse("15-02-2020"), Category= "Fuel", ForEachDollar= 3},
               new PointsPromosion() { PointsPromosionId = "PP003", PromosionName = "Shop Promo", StartDate = DateTime.Parse("01-Mar-2020"), EndDate = DateTime.Parse("01-Mar-2020"), Category= "Shop", ForEachDollar=4 }
               );

            DiscountPromosion discountPromosion1 = new DiscountPromosion()
            {
                DiscountPromotionId = "DP001",
                PromsionName = "Fuel Discount Promo",
                StartDate = DateTime.Parse("01-Jan-2020"),
                EndDate = DateTime.Parse("15-Feb-2020"),
                Discount = 0.2
            };
           // discountPromosion1.Products.Add(product1);
            modelBuilder.Entity<DiscountPromosion>().HasData(
               discountPromosion1,
               new DiscountPromosion() { DiscountPromotionId = "DP002", PromsionName = "Happy Promo", StartDate = DateTime.Parse("02-Mar-2020"), EndDate = DateTime.Parse("20-Mar-2020"), Discount=0.15}
               );


            ProductDiscount productDiscount1 = new ProductDiscount() { ProdDiscountId = "1", DiscountId = discountPromosion1.DiscountPromotionId, ProdId = product1.ProductId };

            modelBuilder.Entity<ProductDiscount>().HasData(new ProductDiscount() { ProdDiscountId = "1", DiscountId = discountPromosion1.DiscountPromotionId, ProdId = product1.ProductId },
                new ProductDiscount() { ProdDiscountId = "2", DiscountId = discountPromosion1.DiscountPromotionId, ProdId = product1.ProductId });

            //base.OnModelCreating(modelBuilder);
        }
       // public DbSet<CaltextLoyaltyProgram.DTOModels.ShoppingListReward> ShoppingListReward { get; set; }
    }
}
