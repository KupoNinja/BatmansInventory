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
        //    Item item = new Item()
        //    {
        //        ItemId = 5,
        //        PartName = "Bat Test",
        //        PartNumber = "BTE-747",
        //        OrderLeadTime = 6,
        //        QuantityOnHand = 4,
        //        SafetyStock = 5,
        //        Created = DateTime.Now,
        //        CreatedBy = "Tester",
        //    };

        //    //Act - Create Item
        //    var repo = new ItemsService(mockContext.Object);
        //    repo.CreateItem(item);

        //    //Assert
        //    mockSet.Verify(m => m.Add(It.IsAny<Item>()), Times.Once);
        //    mockContext.Verify(m => m.SaveChanges(), Times.Once);
        //}

        [Fact]
        public void RetrieveItemsUnderSafetyStock()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "BatmansInventoryDatabase").Options;
            var mock = new Mock<Item>();
            Item item = mock.Object;

            //Act
            var context = new DataContext(options);
            var repo = new ItemsService(context);

            //Assert
            Assert.IsType(repo.GetAllUnderSafetyStock(),
                typeof(List<Item>));
        }
    }
}