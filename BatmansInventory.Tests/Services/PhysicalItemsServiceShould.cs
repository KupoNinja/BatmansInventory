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
    public class PhysicalItemsServiceShould
    {
        const string duplicateSerialNumber = "B052";

        [Fact]
        public void FailPhysicalItemCreationIfSerialNumberIsDuplicate()
        {
            //Arrange
            Mock<IPhysicalItemsRepository> mockPhysicalItemRepo = new Mock<IPhysicalItemsRepository>();
            Mock<IInventoryItemsRepository> mockInventoryItemRepo = new Mock<IInventoryItemsRepository>();
            mockPhysicalItemRepo.Setup(m => m.IsSerialNumberDuplicate(duplicateSerialNumber)).Returns(true);

            PhysicalItemsService sut = new PhysicalItemsService(mockPhysicalItemRepo.Object, mockInventoryItemRepo.Object);
            PhysicalItem physicalItemWithDuplicateSerialNumber = new PhysicalItem()
            {
                PhysicalItemId = 1,
                InventoryItemId = 1,
                SerialNumber = "B052",
                LocationId = 1,
                Value = 9.99m,
                Created = DateTime.Now,
                CreatedBy = "Lucius"
            };

            //Act
            //var isSerialNumberDuplicate = sut.CreatePhysicalItem(physicalItemWithDuplicateSerialNumber);

            //Assert
            Assert.Throws<Exception>(() => sut.CreatePhysicalItem(physicalItemWithDuplicateSerialNumber));
        }
    }
}
