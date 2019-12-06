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
    public class InventoryItemsShould
    {
        private DbContextOptionsBuilder<DataContext> _options;

        public InventoryItemsShould()
        {
            //_options = new DbContextOptionsBuilder<DataContext>()
            //    .UseInMemoryDatabase(databaseName: "BatmansInventory").Options;
        }

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
        public void RetrieveListOfInventoryItemsUnderSafetyStock()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
            using var context = new DataContext(options);
            InventoryItem fakeInventoryItemUnderSafetyStock1 = new InventoryItem()
            {
                PartName = "BataTest",
                PartNumber = "BTE-321",
                OrderLeadTime = 6,
                QuantityOnHand = 5,
                SafetyStock = 10,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            InventoryItem fakeInventoryItemUnderSafetyStock2 = new InventoryItem()
            {
                PartName = "BataTest1",
                PartNumber = "BTE-123",
                OrderLeadTime = 4,
                QuantityOnHand = 2,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            InventoryItem fakeInventoryItemNotUnderSafetyStock = new InventoryItem()
            {
                PartName = "Safe",
                PartNumber = "SAF-321",
                OrderLeadTime = 6,
                QuantityOnHand = 12,
                SafetyStock = 7,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            context.Add(fakeInventoryItemUnderSafetyStock1);
            context.Add(fakeInventoryItemUnderSafetyStock2);
            context.Add(fakeInventoryItemNotUnderSafetyStock);
            context.SaveChanges();

            InventoryItemsRepository repo = new InventoryItemsRepository(context);

            //Act
            var fakeListInventoryItemsUnderSafetyStock = repo.GetAllUnderSafetyStock();

            //Assert
            //Multiple asserts Ok?
            Assert.Contains(fakeInventoryItemUnderSafetyStock1, fakeListInventoryItemsUnderSafetyStock);
            Assert.Contains(fakeInventoryItemUnderSafetyStock2, fakeListInventoryItemsUnderSafetyStock);
            Assert.DoesNotContain(fakeInventoryItemNotUnderSafetyStock, fakeListInventoryItemsUnderSafetyStock);
        }
    }
}