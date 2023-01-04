using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderDetailRepository _orderDetailRepo;

        public OrderController(ILogger<OrderController> logger, IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo)
        {
            _logger = logger;
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
        }

        /// <summary>
        /// 查詢所有訂單及明細資料
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOrders() {
            try
            {

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
