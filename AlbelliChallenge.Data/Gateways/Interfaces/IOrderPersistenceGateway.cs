using AlbelliChallenge.Data.DTOs;
using System.Threading.Tasks;

namespace AlbelliChallenge.Data.Gateways
{
    public interface IOrderPersistenceGateway
    {
        Task<OrderDetailsPersistence> GetOrder(int orderId);
        Task SubmitOrder(OrderDetailsPersistence order);
    }
}