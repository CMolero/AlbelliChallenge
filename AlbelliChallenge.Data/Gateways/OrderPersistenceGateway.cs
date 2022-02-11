using AlbelliChallenge.Data.DTOs;
using System.Threading.Tasks;

namespace AlbelliChallenge.Data.Gateways
{
    public class OrderPersistenceGateway : PersistenceGatewayBase, IOrderPersistenceGateway
    {
        public OrderPersistenceGateway(OrderDetailsRepository orderDetailsRepository) : base (orderDetailsRepository) { }

        public async Task<OrderDetailsPersistence> GetOrder(int orderId) => 
            await Get<OrderDetailsPersistence>(orderId);

        public async Task SubmitOrder(OrderDetailsPersistence order) =>
            await Update<OrderDetailsPersistence>(order);
    }
}