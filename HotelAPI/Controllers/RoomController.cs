using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomRepository _roomRepo;

        public RoomController(ILogger<RoomController> logger, IRoomRepository roomRepo)
        {
            _logger = logger;
            _roomRepo = roomRepo;
        }

        /// <summary>
        /// 查詢所有房間資料
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            try
            {
                var rooms = await _roomRepo.GetRooms();
                return Ok(new
                {
                    Success = true,
                    Message = "All rooms returned.",
                    rooms
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 查詢指定ID的單一房間資料
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRoom(Guid id)
        {
            try
            {
                var room = await _roomRepo.GetRoom(id);
                if (room == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "Room fetched.",
                    room
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增Room資料
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateRoom(RoomForCreationDto room)
        {
            try
            {
                var createdRoom = await _roomRepo.CreateRoom(room);
                return Ok(new
                {
                    Success = true,
                    Message = "Room Created.",
                    createdRoom
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 修改指定ID的Room資料
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRoom(Guid id, RoomForUpdateDto room)
        {
            try
            {
                var dbRoom = await _roomRepo.GetRoom(id);
                if (dbRoom == null)
                    return NotFound();
                await _roomRepo.UpdateRoom(id, room);
                return Ok(new
                {
                    Success = true,
                    Message = "Room Updated.",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 刪除指定ID的Room資料
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            try
            {
                var dbRoom = await _roomRepo.GetRoom(id);
                if (dbRoom == null)
                    return NotFound();
                await _roomRepo.DeleteRoom(id);
                return Ok(new
                {
                    Success = true,
                    Message = "Room Deleted."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///查詢指定OrderDetails的Rid 所在Room資料(訂單明細找房間)
        /// </summary>
        [HttpGet]
        [Route("ByOrderDetailsId/{id}")]
        public async Task<IActionResult> GetRoomByOrderDetailsRId(Guid id)
        {
            try
            {
                var room = await _roomRepo.GetRoomByOrderDetailsRId(id);
                if (room == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    room
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}/MutipleOrderDetailsResults")]
        /// <summary>
        /// 查詢指定OrderDetails所屬的所有Room資料
        /// </summary>
        public async Task<IActionResult> GetRoomOrderDetailsMultipleResults(Guid id)
        {
            try
            {
                var orderdetails = await _roomRepo.GetRoomOrderDetailsMultipleResults(id);
                if (orderdetails == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "Orders Finded.",
                    orderdetails
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
