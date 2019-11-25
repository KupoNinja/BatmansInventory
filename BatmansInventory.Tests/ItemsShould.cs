using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace BatmansInventory.Tests
{
    public class ItemsShould
    {
        [Fact]
        public void CreateNewItem()
        {
            //UseInMemoryDatabase not an extension method?
            //var options = new DbContextOptionsBuilder<DataContext>().Use
            //    .UseInMemoryDatabase(databaseName: "BatmansInventoryDatabase")
            //    .O

            //Arrange
            var mockSet = new Mock<DbSet<Item>>();
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(c => c.Items).Returns(mockSet.Object);
            Item item = new Item()
            {
                ItemId = 5,
                PartName = "Bat Test",
                PartNumber = "BTE-747",
                OrderLeadTime = 6,
                QuantityOnHand = 4,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Tester",
            };

            //Act - Create Item
            var repo = new ItemsService(mockContext.Object);
            repo.CreateItem(item);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Item>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void RetrieveItemsUnderSafetyStock()
        {
            var mockItemsService = new Mock<IItemsService>();
        }
    }
}