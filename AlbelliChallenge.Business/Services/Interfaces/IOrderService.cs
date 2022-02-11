using AlbelliChallenge.Data.Models;
using System.Threading.Tasks;

namespace AlbelliChallenge.Business.Services
{
    public interface IOrderService
    {
        Task<OrderDetails> GetOrder(int orderID);
        Task SubmitOrder(Order orderPlaced);
    }
}