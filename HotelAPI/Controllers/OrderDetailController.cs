using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly ILogger<OrderDetailController> _logger;
        private readonly IOrderDetailRepository _orderDetailRepo;

        public OrderDetailController(ILogger<OrderDetailController> logger, IOrderDetailRepository orderDetailRepo)
        {
            _logger = logger;
            _orderDetailRepo = orderDetailRepo;
        }

        /// <summary>
        /// 查詢所有OrderDetail資料
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOrderDetails()
        {
            try
            {
                var orderDetails = await _orderDetailRepo.GetOrderDetails();
                return Ok(new
                {
                    Success = true,
                    Message = "All orderDetails returned.",
                    orderDetails
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 查詢指定ID的單一OrderDetail資料
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderDetail(String id)
        {
            try
            {
                var orderDetail = await _orderDetailRepo.GetOrderDetail(id);
                if (orderDetail == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "Member fetched.",
                    orderDetail
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///批次新增OrderDetail資料
        /// </summary>
        [HttpPost("multiple")]
        public async Task<IActionResult> CreateOrderDetail(List<OrderDetailForCreationDto> orderDetails)
        {
            try
            {
                await _orderDetailRepo.CreateMultipleOrderDetails(orderDetails);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 批次修改指定ID的OrderDetail資料
        /// </summary>
        //[HttpPatch]
        //[Route("{id}")]
        //public async Task<IActionResult> UpdateOrderDetails(String id, OrderDetailForUpdateDto orderDetails)
        //{
        //    try
        //    {
                
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}



        /// <summary>
        /// 刪除指定ID的OrderDetail資料
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(String id)
        {
            try
            {
                var dbOrderDetail = await _orderDetailRepo.GetOrderDetail(id);
                if (dbOrderDetail == null)
                    return NotFound();
                await _orderDetailRepo.DeletOrderDetail(id);
                return Ok(new
                {
                    Success = true,
                    Message = "OrderDetail Deleted."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
