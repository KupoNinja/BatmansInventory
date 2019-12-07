using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace BatmansInventory.Tests
{
    public class InventoryItemsShould
    {
        //private DbContextOptionsBuilder<DataContext> _options;

        //public InventoryItemsShould()
        //{
        //    //_options = new DbContextOptionsBuilder<DataContext>()
        //    //    .UseInMemoryDatabase(databaseName: "BatmansInventory").Options;
        //}

        [Fact]
        public void CreateNewInventoryItemIntoTheDatabase()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
            using var context = new DataContext(options);
            InventoryItem fakeInventoryItem = new InventoryItem()
            {
                PartName = "Bat Test",
                PartNumber = "BTE-747",
                OrderLeadTime = 6,
                QuantityOnHand = 4,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Tester",
            };
            InventoryItemsRepository repo = new InventoryItemsRepository(context);

            //Act
            var newFakeInventoryItem = repo.CreateInventoryItem(fakeInventoryItem);
            var fakeListOfInventoryItems = repo.GetAll();

            //Assert
            Assert.Contains(newFakeInventoryItem, fakeListOfInventoryItems);
        }

        [Fact]
        public void UpdateInventoryItem()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
            using var context = new DataContext(options);
            InventoryItem fakeInventoryItem = new InventoryItem()
            {
                PartName = "Bat Test",
                PartNumber = "BTE-747",
                OrderLeadTime = 6,
                QuantityOnHand = 4,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Tester",
            };
            InventoryItemsRepository repo = new InventoryItemsRepository(context);
            context.Add(fakeInventoryItem);
            context.SaveChanges();

            var fakeInventoryItemToUpdate = repo.GetById(1);
            fakeInventoryItemToUpdate.PartName = "Updated Item";

            //Act
            var updatedFakeInventoryItem = repo.UpdateInventoryItem(fakeInventoryItemToUpdate);

            //Assert
            Assert.Equal("Updated Item", updatedFakeInventoryItem.PartName);
        }

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

        //Breaks when you run all tests but passes when tested individually
        //Does the DataContext persist?
        [Fact]
        public void RetrieveInventoryItemById()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
            using var context = new DataContext(options);
            InventoryItem fakeInventoryItemToRetrieve = new InventoryItem()
            {
                PartName = "BataTest",
                PartNumber = "BTE-321",
                OrderLeadTime = 6,
                QuantityOnHand = 5,
                SafetyStock = 10,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            InventoryItem fakeInventoryItem1 = new InventoryItem()
            {
                PartName = "BataTest1",
                PartNumber = "BTE-123",
                OrderLeadTime = 4,
                QuantityOnHand = 2,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            InventoryItem fakeInventoryItem2 = new InventoryItem()
            {
                PartName = "Safe",
                PartNumber = "SAF-321",
                OrderLeadTime = 6,
                QuantityOnHand = 12,
                SafetyStock = 7,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            context.Add(fakeInventoryItemToRetrieve);
            context.Add(fakeInventoryItem1);
            context.Add(fakeInventoryItem2);
            context.SaveChanges();

            InventoryItemsRepository repo = new InventoryItemsRepository(context);

            //Act
            var fakeInventoryItem = repo.GetById(1);
            var fakeInventoryItemReturnedString = JsonConvert.SerializeObject(fakeInventoryItem);
            var fakeInventoryItemToRetrieveString = JsonConvert.SerializeObject(fakeInventoryItemToRetrieve);
            var fakeInventoryItem1String = JsonConvert.SerializeObject(fakeInventoryItem1);
            var fakeInventoryItem2String = JsonConvert.SerializeObject(fakeInventoryItem2);

            //Assert
            Assert.NotEqual(fakeInventoryItem1String, fakeInventoryItemReturnedString);
            Assert.NotEqual(fakeInventoryItem2String, fakeInventoryItemReturnedString);
            Assert.Equal(fakeInventoryItemReturnedString, fakeInventoryItemToRetrieveString);
        }

        [Fact]
        public void RetrieveInventoryItemByPartNumber()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
            using var context = new DataContext(options);
            InventoryItem fakeInventoryItemToRetrieve = new InventoryItem()
            {
                PartName = "BataTest",
                PartNumber = "BTE-321",
                OrderLeadTime = 6,
                QuantityOnHand = 5,
                SafetyStock = 10,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            InventoryItem fakeInventoryItem1 = new InventoryItem()
            {
                PartName = "BataTest1",
                PartNumber = "BTE-123",
                OrderLeadTime = 4,
                QuantityOnHand = 2,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            InventoryItem fakeInventoryItem2 = new InventoryItem()
            {
                PartName = "Safe",
                PartNumber = "SAF-321",
                OrderLeadTime = 6,
                QuantityOnHand = 12,
                SafetyStock = 7,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            context.Add(fakeInventoryItem1);
            context.Add(fakeInventoryItem2);
            context.Add(fakeInventoryItemToRetrieve);
            context.SaveChanges();

            InventoryItemsRepository repo = new InventoryItemsRepository(context);

            //Act
            var fakeInventoryItem = repo.GetByPartNumber(fakeInventoryItemToRetrieve.PartNumber);
            var fakeInventoryItemReturnedString = JsonConvert.SerializeObject(fakeInventoryItem);
            var fakeInventoryItemToRetrieveString = JsonConvert.SerializeObject(fakeInventoryItemToRetrieve);
            var fakeInventoryItem1String = JsonConvert.SerializeObject(fakeInventoryItem1);
            var fakeInventoryItem2String = JsonConvert.SerializeObject(fakeInventoryItem2);

            //Assert
            Assert.NotEqual(fakeInventoryItem1String, fakeInventoryItemReturnedString);
            Assert.NotEqual(fakeInventoryItem2String, fakeInventoryItemReturnedString);
            Assert.Equal(fakeInventoryItemReturnedString, fakeInventoryItemToRetrieveString);
        }
    }
}