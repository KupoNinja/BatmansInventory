using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using System;
using Xunit;

namespace BatmansInventory.Tests
{
    public class PhysicalItemsShould
    {
        private readonly DataContext _db;

        [Fact]
        public void ImplementIPhysicalItem()
        {
            PhysicalItem sut = new PhysicalItem();

            Assert.IsAssignableFrom<IPhysicalItem>(sut);
        }

        [Fact]
        public void GetTotalValue()
        {
            PhysicalItemsService sut = new PhysicalItemsService(_db);

            Assert.NotEqual(0, sut.GetTotalValueByItem(3));
        }
    }
}