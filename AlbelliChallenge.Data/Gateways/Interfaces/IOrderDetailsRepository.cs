using AlbelliChallenge.Data.DTOs;

namespace AlbelliChallenge.Data.Gateways.Interfaces
{
    public interface IOrderDetailsRepository
    {
        OrderDetailsPersistence GetOrder(int orderId);
        bool InsertOrder(OrderDetailsPersistence orderDetailsPersistence);
    }
}