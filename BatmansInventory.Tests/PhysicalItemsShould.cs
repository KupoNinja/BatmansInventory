using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

// Refactor these tests. This was used to learn xUnit and attempt Moq
// Can look for reference but most of these are brittle

namespace BatmansInventory.Tests
{
    public class PhysicalItemsShould
    {
        private BatmansInventoryContext GetPopulatedInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BatmansInventoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new BatmansInventoryContext(options);
            PhysicalItem fakePhysicalItemSameLocation1 = new PhysicalItem()
            {
                PhysicalItemId = 1,
                InventoryItemId = 1,
                SerialNumber = "A001",
                LocationId = 1,
                Value = 9.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };
            PhysicalItem fakePhysicalItemSameLocation2 = new PhysicalItem()
            {
                PhysicalItemId = 2,
                InventoryItemId = 1,
                SerialNumber = "A002",
                LocationId = 1,
                Value = 9.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };
            PhysicalItem fakePhysicalItemDifferentLocation = new PhysicalItem()
            {
                PhysicalItemId = 3,
                InventoryItemId = 2,
                SerialNumber = "B001",
                LocationId = 3,
                Value = 19.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };
            context.Add(fakePhysicalItemSameLocation1);
            context.Add(fakePhysicalItemSameLocation2);
            context.Add(fakePhysicalItemDifferentLocation);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void RetrieveListOfPhysicalItemsByLocation()
        {
            //Arrange
            var context = GetPopulatedInMemoryDbContext();

            PhysicalItemsRepository sut = new PhysicalItemsRepository(context);
            var fakePhysicalItemSameLocation1 = sut.GetById(1);
            var fakePhysicalItemSameLocation2 = sut.GetById(2);
            var fakePhysicalItemDifferentLocation = sut.GetById(3);

            //Act
            var fakeListByLocation = sut.GetByLocation(1);

            //Assert
            Assert.Contains(fakePhysicalItemSameLocation1, fakeListByLocation);
            Assert.Contains(fakePhysicalItemSameLocation2, fakeListByLocation);
            Assert.DoesNotContain(fakePhysicalItemDifferentLocation, fakeListByLocation);
        }

        //Breaks when you run all tests but passes when tested individually
        [Fact]
        public void GiveTotalValueOfAnItem()
        {
            //Arrange
            var context = GetPopulatedInMemoryDbContext();

            PhysicalItemsRepository sut = new PhysicalItemsRepository(context);
            var fakePhysicalItem1 = sut.GetById(1);

            //Act
            var totalValue = sut.GetTotalValueByInventoryItem(fakePhysicalItem1.InventoryItemId);

            //Assert
            Assert.Equal(19.98m, totalValue);
        }
    }
}