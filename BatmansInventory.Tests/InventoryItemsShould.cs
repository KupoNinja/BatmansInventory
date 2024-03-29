using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

// Refactor these tests. This was used to learn xUnit and attempt Moq
// Can look for reference but most of these are brittle

namespace BatmansInventory.Tests
{
    public class InventoryItemsShould
    {
        // Use to refactor after you get tests to pass
        //private Mock<IInventoryItemsRepository> _mockRepo;
        //private IInventoryItemsService sut;

        const int fakeInventoryItemId = 1;
        private List<InventoryItem> _fakeInventoryItemsList;

        public InventoryItemsShould()
        {
            _fakeInventoryItemsList = new List<InventoryItem>();
            //Fill in w/ _mockRepo & sut
        }

        private void PopulateFakeInventoryItemsList()
        {
            InventoryItem fakeInventoryItem = new InventoryItem()
            {
                InventoryItemId = fakeInventoryItemId,
                PartName = "Bat Test",
                PartNumber = "BTE-747",
                OrderLeadTime = 6,
                QuantityOnHand = 4,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Tester",
            };
            InventoryItem fakeInventoryItemToRetrieve = new InventoryItem()
            {
                InventoryItemId = 2,
                PartName = "BataTest",
                PartNumber = "BTE-321",
                OrderLeadTime = 6,
                QuantityOnHand = 5,
                SafetyStock = 10,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            InventoryItem fakeInventoryItemUnderSafetyStock1 = new InventoryItem()
            {
                InventoryItemId = 3,
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
                InventoryItemId = 4,
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
                InventoryItemId = 5,
                PartName = "Safe",
                PartNumber = "SAF-321",
                OrderLeadTime = 6,
                QuantityOnHand = 12,
                SafetyStock = 7,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };

            _fakeInventoryItemsList.Add(fakeInventoryItem);
            _fakeInventoryItemsList.Add(fakeInventoryItemToRetrieve);
            _fakeInventoryItemsList.Add(fakeInventoryItemUnderSafetyStock1);
            _fakeInventoryItemsList.Add(fakeInventoryItemUnderSafetyStock2);
            _fakeInventoryItemsList.Add(fakeInventoryItemNotUnderSafetyStock);
        }

        private BatmansInventoryContext GetPopulatedInMemoryDbContext()
        {
            // Naming in-memory db by GUID so every test ran is a new db so it's not affected by previous runs
            var options = new DbContextOptionsBuilder<BatmansInventoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new BatmansInventoryContext(options);

            InventoryItem fakeInventoryItem = new InventoryItem()
            {
                InventoryItemId = 1,
                PartName = "Bat Test",
                PartNumber = "BTE-747",
                OrderLeadTime = 6,
                QuantityOnHand = 4,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Tester",
            };
            InventoryItem fakeInventoryItemToRetrieve = new InventoryItem()
            {
                InventoryItemId = 2,
                PartName = "BataTest",
                PartNumber = "BTE-321",
                OrderLeadTime = 6,
                QuantityOnHand = 5,
                SafetyStock = 10,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };
            InventoryItem fakeInventoryItemUnderSafetyStock1 = new InventoryItem()
            {
                InventoryItemId = 3,
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
                InventoryItemId = 4,
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
                InventoryItemId = 5,
                PartName = "Not Safe",
                PartNumber = "SAF-321",
                OrderLeadTime = 6,
                QuantityOnHand = 12,
                SafetyStock = 7,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };

            context.Add(fakeInventoryItem);
            context.Add(fakeInventoryItemToRetrieve);
            context.Add(fakeInventoryItemUnderSafetyStock1);
            context.Add(fakeInventoryItemUnderSafetyStock2);
            context.Add(fakeInventoryItemNotUnderSafetyStock);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void CreateNewInventoryItemIntoTheDatabase()
        {
            //Arrange
            var context = GetPopulatedInMemoryDbContext();
            //PopulateFakeInventoryItemsList();
            var fakeList = _fakeInventoryItemsList;

            InventoryItem fakeInventoryItemToCreate = new InventoryItem()
            {
                InventoryItemId = 6,
                PartName = "Shiny",
                PartNumber = "SHI-747",
                OrderLeadTime = 6,
                QuantityOnHand = 4,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Tester",
            };

            //Mock<IInventoryItemsRepository> mockRepo = new Mock<IInventoryItemsRepository>();
            //mockRepo.Setup(m => m.CreateInventoryItem(It.IsAny<InventoryItem>())).Returns(fakeInventoryItemToCreate);
            //mockRepo.Setup(m => m.GetAll()).Returns(() => fakeList);

            //InventoryItemsService sut = new InventoryItemsService(mockRepo.Object);
            IInventoryItemsRepository sut = new InventoryItemsRepository(context);

            //Act
            var newFakeInventoryItem = sut.CreateInventoryItem(fakeInventoryItemToCreate);
            var fakeListOfInventoryItems = sut.GetAll();

            //Assert
            Assert.Contains(newFakeInventoryItem, fakeListOfInventoryItems);
        }

        [Fact]
        public void ReturnInventoryItemByItsId()
        {
            //Arrange
            //var context = GetPopulatedInMemoryDbContext();
            bool hasGetByIdBeenCalled = false;

            InventoryItem fakeInventoryItemToRetrieve = new InventoryItem()
            {
                InventoryItemId = 2,
                PartName = "BataTest",
                PartNumber = "BTE-321",
                OrderLeadTime = 6,
                QuantityOnHand = 5,
                SafetyStock = 10,
                Created = DateTime.Now,
                CreatedBy = "Alfred"
            };

            Mock<IInventoryItemsRepository> mockRepo = new Mock<IInventoryItemsRepository>();
            mockRepo.Setup(m => m.GetById(It.IsAny<int>())).Callback<int>(i => { hasGetByIdBeenCalled = true; }).Returns(fakeInventoryItemToRetrieve);

            InventoryItemsService sut = new InventoryItemsService(mockRepo.Object);

            //Act
            var fakeInventoryItemReturned = sut.GetById(fakeInventoryItemToRetrieve.InventoryItemId);

            //Assert
            Assert.True(hasGetByIdBeenCalled);
            Assert.Equal(fakeInventoryItemToRetrieve.InventoryItemId, fakeInventoryItemReturned.InventoryItemId);
        }

        //[Fact]
        //public void RetrieveInventoryItemByPartNumber()
        //{
        //    //Arrange
        //    var context = GetPopulatedInMemoryDbContext();

        //    InventoryItemsRepository sut = new InventoryItemsRepository(context);

        //    //Act
        //    var fakeInventoryItemToRetrieve = sut.GetByPartNumber("BTG-001");

        //    //Assert
        //    Assert.Equal("BTG-001", fakeInventoryItemToRetrieve.PartNumber);
        //}

        [Fact]
        public void UpdateTheSelectedInventoryItem()
        {
            //Arrange
            var context = GetPopulatedInMemoryDbContext();

            InventoryItemsRepository sut = new InventoryItemsRepository(context);

            var fakeInventoryItemToUpdate = sut.GetById(1);
            fakeInventoryItemToUpdate.PartName = "Updated Item";

            //Act
            var updatedFakeInventoryItem = sut.UpdateInventoryItem(fakeInventoryItemToUpdate);

            //Assert
            Assert.Equal("Updated Item", updatedFakeInventoryItem.PartName);
        }

        [Fact]
        public void RetrieveListOfInventoryItemsUnderSafetyStock()
        {
            //Arrange
            var context = GetPopulatedInMemoryDbContext();

            InventoryItemsRepository sut = new InventoryItemsRepository(context);

            //Act
            var fakeListInventoryItemsUnderSafetyStock = sut.GetAllUnderSafetyStock();

            var fakeInventoryItemUnderSafetyStock1 = sut.GetById(3);
            var fakeInventoryItemUnderSafetyStock2 = sut.GetById(4);
            var fakeInventoryItemNotUnderSafetyStock = sut.GetById(5);

            //Assert            
            Assert.Contains(fakeInventoryItemUnderSafetyStock1, fakeListInventoryItemsUnderSafetyStock);
            Assert.Contains(fakeInventoryItemUnderSafetyStock2, fakeListInventoryItemsUnderSafetyStock);
            Assert.DoesNotContain(fakeInventoryItemNotUnderSafetyStock, fakeListInventoryItemsUnderSafetyStock);
        }
    }
}