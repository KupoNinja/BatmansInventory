using BatmansInventory.API.Controllers;
using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BatmansInventory.Tests.Controllers
{
    public class DashboardControllerShould
    {
        private Mock<IInventoryItemsService> mockInventoryItemsService;
        private Mock<IPhysicalItemsService> mockPhysicalItemsService;
        private DashboardController sut;

        [Fact]
        public void CallGetAllInventoryItemsUnderSafeteyStock()
        {
            //Arrange
            //var list = new List<InventoryItem>();

            mockInventoryItemsService  = new Mock<IInventoryItemsService>();
            mockPhysicalItemsService = new Mock<IPhysicalItemsService>();
            //mockInventoryItemsService.Setup(m => m.GetAllUnderSafetyStock()).Returns(list);

            sut = new DashboardController(mockInventoryItemsService.Object, mockPhysicalItemsService.Object);

            var listUnderSafetyStock = sut.GetAllInventoryItemsUnderSafetyStock();

            mockInventoryItemsService.Verify(m => m.GetAllUnderSafetyStock());
        }

    }
}
