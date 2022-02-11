using AlbelliChallenge.Business.Services;
using AlbelliChallenge.Data.DTOs;
using AlbelliChallenge.Data.Gateways;
using AlbelliChallenge.Data.Models;
using AlbelliChallenge.Test.Extensions;
using Moq;
using System;
using Xunit;

namespace AlbelliChallenge.Test
{
    public class SubmitOrderTests : ServiceTestsBase
    {
        private readonly Mock<IOrderPersistenceGateway> _mockOrderPersistenceGateway;
        private readonly OrderService _sut;

        public SubmitOrderTests()
        {
            _mockOrderPersistenceGateway = new Mock<IOrderPersistenceGateway>();
            _sut = new OrderService(_mockOrderPersistenceGateway.Object, _mapper);
        }

        [Fact]
        public async void Try_SubmitOrder_Valid_Order_Then_SaveOrder()
        {
            var request = new Order().Build();
            
            await _sut.SubmitOrder(request);

            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Once());
        }

        [Fact]
        public async void Try_SubmitOrder_Order_WithoutOrderId_Then_ThrowArgumentException()
        {
            var request = new Order().Build().WithInvalidOrderId();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.SubmitOrder(request));

            Assert.Equal("OrderId is required", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Never());
        }

        [Fact]
        public async void Try_SubmitOrder_Order_WithoutProductType_Then_ThrowArgumentException()
        {
            var request = new Order().Build().WithoutProductType();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.SubmitOrder(request));

            Assert.Equal("ProductType is not valid", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Never());
        }

        [Fact]
        public async void Try_SubmitOrder_Order_WithInvalidProductType_Then_ThrowArgumentException()
        {
            var request = new Order().Build().WithInvalidProductType();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.SubmitOrder(request));

            Assert.Equal("ProductType is not valid", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Never());
        }

        [Fact]
        public async void Try_SubmitOrder_Order_WithoutQuantity_Then_ThrowArgumentException()
        {
            var request = new Order().Build().WithoutQuantity();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.SubmitOrder(request));

            Assert.Equal("Quantity is required", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Never());
        }
    }
}