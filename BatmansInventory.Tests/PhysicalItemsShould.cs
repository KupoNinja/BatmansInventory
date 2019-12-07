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
        [Fact]
        public void RetrieveListOfPhysicalItemsByLocation()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
            using var context = new DataContext(options);
            PhysicalItem fakePhysicalItemSameLocation1 = new PhysicalItem()
            {
                InventoryItemId = 1,
                SerialNumber = "A001",
                LocationId = 1,
                Value = 9.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };
            PhysicalItem fakePhysicalItemSameLocation2 = new PhysicalItem()
            {
                InventoryItemId = 2,
                SerialNumber = "A002",
                LocationId = 1,
                Value = 9.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };
            PhysicalItem fakePhysicalItemDifferentLocation = new PhysicalItem()
            {
                InventoryItemId = 3,
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

            PhysicalItemsRepository repo = new PhysicalItemsRepository(context);

            //Act
            var fakeListByLocation = repo.GetByLocation(fakePhysicalItemSameLocation1.LocationId);

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
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("BatmansInventoryDatabase").Options;
            using var context = new DataContext(options);
            PhysicalItem fakePhysicalItem1 = new PhysicalItem()
            {
                InventoryItemId = 1,
                SerialNumber = "A001",
                LocationId = 1,
                Value = 9.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };
            PhysicalItem fakePhysicalItem2 = new PhysicalItem()
            {
                InventoryItemId = 3,
                SerialNumber = "A002",
                LocationId = 1,
                Value = 9.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };
            PhysicalItem fakePhysicalItem3 = new PhysicalItem()
            {
                InventoryItemId = 3,
                SerialNumber = "B001",
                LocationId = 3,
                Value = 19.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };
            context.Add(fakePhysicalItem1);
            context.Add(fakePhysicalItem2);
            context.Add(fakePhysicalItem3);
            context.SaveChanges();

            PhysicalItemsRepository repo = new PhysicalItemsRepository(context);

            //Act
            var totalValue = repo.GetTotalValueByInventoryItem(fakePhysicalItem2.InventoryItemId);

            //Assert
            Assert.Equal(29.98m, totalValue);
        }
    }
}