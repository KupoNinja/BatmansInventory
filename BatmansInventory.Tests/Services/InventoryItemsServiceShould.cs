using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BatmansInventory.Tests.Services
{
    public class InventoryItemsServiceShould
    {
        const string duplicatePartNumber = "BTG-001";
        const string validPartNumber = "ZZZ-999";
        
        private Mock<IInventoryItemsRepository> mockRepo;
        private InventoryItemsService sut;

        [Fact]
        public void FailInventoryItemCreationIfPartNumberIsDuplicate()
        {
            //Arrange
            mockRepo = new Mock<IInventoryItemsRepository>();
            mockRepo.Setup(m => m.IsPartNumberDuplicate(duplicatePartNumber)).Returns(true);

            sut = new InventoryItemsService(mockRepo.Object);

            InventoryItem inventoryItemWithDuplicatePartNumber = new InventoryItem()
            {
                InventoryItemId = 1,
                PartName = "Bat Test",
                PartNumber = "BTG-001",
                OrderLeadTime = 6,
                QuantityOnHand = 4,
                SafetyStock = 5,
                Created = DateTime.Now,
                CreatedBy = "Tester",
            };

            //Act
            Assert.Throws<Exception>(() => sut.CreateInventoryItem(inventoryItemWithDuplicatePartNumber));
        }
    }
}
