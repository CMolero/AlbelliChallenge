using AlbelliChallenge.Business.Models;
using AlbelliChallenge.Business.Services;
using AlbelliChallenge.Data.DTOs;
using AlbelliChallenge.Data.Gateways;
using AlbelliChallenge.Test.Extensions;
using Moq;
using System;
using Xunit;

namespace AlbelliChallenge.Test.ServiceTests
{
    public class GetOrderDetailsTests : ServiceTestsBase
    {
        private readonly Mock<IOrderPersistenceGateway> _mockOrderPersistenceGateway;
        private readonly OrderService _sut;

        public GetOrderDetailsTests()
        {
            _mockOrderPersistenceGateway = new Mock<IOrderPersistenceGateway>();
            _sut = new OrderService(_mockOrderPersistenceGateway.Object, _mapper);
        }

        [Fact]
        public async void When_GetOrderDetails_HasValidOrderId_Then_Returns_OrderDetails()
        {
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build());

            var result = await _sut.GetOrder(orderId: 1);

            _mockOrderPersistenceGateway.Verify(s => s.GetOrder(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void When_GetOrderDetails_HasInvalidOrderId_Then_ThrowArgumentException()
        {
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build());

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetOrder(orderId: 0));

            Assert.Equal("OrderId is not valid", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.GetOrder(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public async void When_GetOrderDetails_DoesNotContainsRecords_Then_DoesNotReturns_OrderDetails()
        {
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithNull());

            var result = await _sut.GetOrder(orderId: 1);

            Assert.Empty(result.Products);
            Assert.Equal(0, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_PhotoBooks_Then_CalculateRequiredBinWidth()
        {
            var expected = 1425;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(Enums.ProductTypes.PhotoBook, 75));

            var result = await _sut.GetOrder(orderId: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_Calendars_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.Calendar;
            var quantity = 40;
            var expected = 400;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrder(orderId: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_Canvas_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.Canvas;
            var quantity = 4;
            var expected = 64;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrder(orderId: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_SetOfGreetingCards_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.Cards;
            var quantity = 10;
            var expected = 47;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrder(orderId: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_Mug_AsQuantityMultipleOfFour_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.Mug;
            var quantity = 24;
            var expected = 564;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrder(orderId: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_AllProducts_WhichContainsOneMug_Then_CalculateRequiredBinWidth()
        {
            var quantityOfMug = 1;
            var expected = 728.1;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrder(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithAllProducts(quantityOfMug));

            var result = await _sut.GetOrder(orderId: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }
    }
}
