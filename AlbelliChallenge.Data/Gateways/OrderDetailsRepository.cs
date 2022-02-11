using AlbelliChallenge.Data.DTOs;
using AlbelliChallenge.Data.Gateways.Interfaces;
using System.Collections.Generic;

namespace AlbelliChallenge.Data.Gateways
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly List<OrderDetailsPersistence> _orderDetailsPersistence;

        public OrderDetailsRepository() 
        {
            _orderDetailsPersistence = new List<OrderDetailsPersistence>();
        }

        public OrderDetailsPersistence GetOrder(int orderId) =>
            _orderDetailsPersistence.Find(x => x.OrderId == orderId);

        public bool InsertOrder(OrderDetailsPersistence orderDetailsPersistence)
        {
            var repositoryCount = _orderDetailsPersistence.Count;

            _orderDetailsPersistence.Add(orderDetailsPersistence);

            return repositoryCount < _orderDetailsPersistence.Count;
        }     
    }
}