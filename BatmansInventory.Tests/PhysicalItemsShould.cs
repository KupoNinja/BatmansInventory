using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace BatmansInventory.Tests
{
    public class PhysicalItemsShould
    {
        private DataContext GetPopulatedInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
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

            PhysicalItemsRepository repo = new PhysicalItemsRepository(context);
            var fakePhysicalItemSameLocation1 = repo.GetById(1);
            var fakePhysicalItemSameLocation2 = repo.GetById(2);
            var fakePhysicalItemDifferentLocation = repo.GetById(3);

            //Act
            var fakeListByLocation = repo.GetByLocation(1);

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

            PhysicalItemsRepository repo = new PhysicalItemsRepository(context);
            var fakePhysicalItem1 = repo.GetById(1);

            //Act
            var totalValue = repo.GetTotalValueByInventoryItem(fakePhysicalItem1.InventoryItemId);

            //Assert
            Assert.Equal(19.98m, totalValue);
        }
    }
}