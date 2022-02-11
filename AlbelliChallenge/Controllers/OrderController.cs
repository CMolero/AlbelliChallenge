using AlbelliChallenge.Business.Services;
using AlbelliChallenge.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AlbelliChallenge.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : MainController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService)); ;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        public async Task<ActionResult> SubmitOrder([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                return await ExecutePost(() => _orderService.SubmitOrder(order)).ConfigureAwait(false);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<OrderDetails>> GetOrderDetails(int orderId)
        {
            if (ModelState.IsValid)
            {
                return await ExecuteGet(() => _orderService.GetOrder(orderId));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}