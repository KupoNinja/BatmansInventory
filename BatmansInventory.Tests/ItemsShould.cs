using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BatmansInventory.Tests
{
    public class ItemsShould
    {
        //private DbContextOptionsBuilder<DataContext> _options;

        //public ItemsShould()
        //{
        //    _options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: "BatmansInventory").Options;
        //}

        //[Fact]
        //public void CreateNewItem()
        //{
        //    //Arrange
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
        //    using var context = new DataContext(options);
        //    Item fakeItem = new Item()
        //    {
        //        PartName = "Bat Test",
        //        PartNumber = "BTE-747",
        //        OrderLeadTime = 6,
        //        QuantityOnHand = 4,
        //        SafetyStock = 5,
        //        Created = DateTime.Now,
        //        CreatedBy = "Tester",
        //    };
        //    ItemsService repo = new ItemsService(context);
            
        //    //Act
        //    repo.CreateItem(fakeItem);

        //    //Assert
        //    Assert.
        //}

        [Fact]
        public void RetrieveListOfItemsUnderSafetyStock()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
            using var context = new DataContext(options);
            Item fakeItemUnderSafetyStock1 = new Item()
            {
                PartName = "BataTest",
                PartNumber = "BTE-321",
                OrderLeadTime = 6,
                QuantityOnHand = 5,
                SafetyStock = 10,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            Item fakeItemUnderSafetyStock2 = new Item()
            {
                PartName = "BataTest1",
                PartNumber = "BTE-123",
                OrderLeadTime = 4,
                QuantityOnHand = 2,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            Item fakeItemNotUnderSafetyStock = new Item()
            {
                PartName = "Safe",
                PartNumber = "SAF-321",
                OrderLeadTime = 6,
                QuantityOnHand = 12,
                SafetyStock = 7,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            context.Add(fakeItemUnderSafetyStock1);
            context.Add(fakeItemUnderSafetyStock2);
            context.Add(fakeItemNotUnderSafetyStock);
            context.SaveChanges();

            ItemsService repo = new ItemsService(context);

            //Act
            var fakeListItemsUnderSafetyStock = repo.GetAllUnderSafetyStock();

            //Assert
            //Multiple asserts Ok?
            Assert.Contains(fakeItemUnderSafetyStock1, fakeListItemsUnderSafetyStock);
            Assert.Contains(fakeItemUnderSafetyStock2, fakeListItemsUnderSafetyStock);
            Assert.DoesNotContain(fakeItemNotUnderSafetyStock, fakeListItemsUnderSafetyStock);
        }
    }
}