using AlbelliChallenge.Data.DTOs;
using AlbelliChallenge.Data.Gateways;
using AlbelliChallenge.Data.Models;
using AutoMapper;
using System.Threading.Tasks;

namespace AlbelliChallenge.Business.Services
{
    public class OrderService : OrderServiceBase, IOrderService
    {
        private readonly IOrderPersistenceGateway _orderPersistence;

        public OrderService(IOrderPersistenceGateway orderPersistenceGateway, IMapper mapper)
        {
            _orderPersistence = orderPersistenceGateway;
            _mapper = mapper;
        }

        public async Task<OrderDetails> GetOrder(int orderId)
        {
            var orderRequiredBinWidth = new OrderDetailsPersistence();

            if (IsValidOrderId(orderId))
            {
                var orderDetails = await _orderPersistence.GetOrder(orderId);

                if (orderDetails != null)
                {
                    orderRequiredBinWidth = CalculateRequiredBinWidth(orderDetails);
                }
            }

            return _mapper.Map<OrderDetailsPersistence, OrderDetails>(orderRequiredBinWidth);
        }

        public async Task SubmitOrder(Order order)
        {
            if (IsValidOrder(order))
            {
                var newOrder = _mapper.Map<Order, OrderDetailsPersistence>(order);
                await _orderPersistence.SubmitOrder(newOrder);
            }

        }
    }
}