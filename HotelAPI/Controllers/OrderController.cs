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
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderRepo.GetOrders();
                return Ok(new
                {
                    Success = true,
                    Message = "all orders returned",
                    orders,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        ///// <summary>
        ///// 查詢所有訂單資料
        ///// </summary>
        //[HttpGet]
        //public async Task<IActionResult> GetOrders()
        //{
        //    try
        //    {
        //        var orders = await _orderRepo.GetOrders();
        //        return Ok(new
        //        {
        //            Success = true,
        //            Message = "All Orders returned.",
        //            orders,
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        /// <summary>
        /// 查詢指定ID的單一訂單資料
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder(String id)
        {
            try
            {
                var order = await _orderRepo.GetOrder(id);
                if (order == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "Order fetched.",
                    order,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增Order資料
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderForCreationDto comment)
        {
            try
            {
                var createOrder = await _orderRepo.CreateOrder(comment);
                return Ok(new
                {
                    Success = true,
                    Message = "Order created.",
                    createOrder,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        /// <summary>
        /// 修改指定ID的Order資料
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOrder(String id, OrderForUpdateDto order)
        {
            try
            {
                var dbOrder = await _orderRepo.GetOrder(id);
                if (dbOrder == null)
                    return NotFound();
                await _orderRepo.UpdateOrder(id, order);
                return Ok(new
                {
                    Success = true,
                    Message = "Order updated.",
                    order,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 刪除指定ID的Order資料
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrder(String id)
        {
            try
            {
                var dbOrder = await _orderRepo.GetOrder(id);
                if (dbOrder == null)
                    return NotFound();
                await _orderRepo.DeleteOrder(id);
                return Ok(new
                {
                    Success = true,
                    Message = "Order deleted."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
