using AlbelliChallenge.Data.DTOs;
using System.Threading.Tasks;

namespace AlbelliChallenge.Data.Gateways
{
    public class PersistenceGatewayBase
    {
        protected OrderDetailsRepository _orderDetailsRepository;

        public PersistenceGatewayBase(OrderDetailsRepository orderDetailsRepository) 
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public async Task<OrderDetailsPersistence> Get<T>(int param) 
        {
            return await Task.FromResult(_orderDetailsRepository.GetOrder(param));
        }

        public async Task Update<T>(OrderDetailsPersistence orderDetailsPersistence)
        {
            await Task.FromResult(_orderDetailsRepository.InsertOrder(orderDetailsPersistence));
        }
    }
}