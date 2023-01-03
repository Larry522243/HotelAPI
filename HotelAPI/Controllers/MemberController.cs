using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberRepository _memberRepo;

        public MemberController(ILogger<MemberController> logger, IMemberRepository memberRepo)
        {
            _logger = logger;
            _memberRepo = memberRepo;
        }

        /// <summary>
        /// 查詢所有會員資料
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                var members = await _memberRepo.GetMembers();
                return Ok(new
                {
                    Success = true,
                    Message = "All members returned.",
                    members
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 查詢指定ID的單一會員資料
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMember(Guid id)
        {
            try
            {
                var member = await _memberRepo.GetMember(id);
                if (member == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "Member fetched.",
                    member
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增Member資料
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateMember(MemberForCreationDto member)
        {
            try
            {
                var createdMember = await _memberRepo.CreateMember(member);
                return Ok(new
                {
                    Success = true,
                    Message = "Member Created.",
                    createdMember,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 修改指定ID的Member資料
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMember(Guid id, MemberForUpdateDto member)
        {
            try
            {
                var dbMember = await _memberRepo.GetMember(id);
                if (dbMember == null)
                    return NotFound();
                await _memberRepo.UpdateMember(id, member);
                return Ok(new
                {
                    Success = true,
                    Message = "Member Updated.",
                    member,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 刪除指定ID的Member資料
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMember(Guid id)
        {
            try
            {
                var dbMember = await _memberRepo.GetMember(id);
                if (dbMember == null)
                    return NotFound();
                await _memberRepo.DeleteMember(id);
                return Ok(new
                {
                    Success = true,
                    Message = "Member Deleted."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///查詢指定Order的Mid 所在Member資料(訂單找會員)
        /// </summary>
        [HttpGet]
        [Route("ByOrderId/{id}")]
        public async Task<IActionResult> GetMemberByOrderMId(String id)
        {
            try
            {
                var member = await _memberRepo.GetMemberByOrderMId(id);
                if (member == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    member
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}/MutipleOrderResults")]
        /// <summary>
        /// 查詢指定Member所屬的所有Order資料
        /// </summary>
        public async Task<IActionResult> GetMemberOrderMultipleResults(Guid id)
        {
            try
            {
                var orders = await _memberRepo.GetMemberOrderMultipleResults(id);
                if (orders == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "Orders Finded.",
                    orders
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{partIDNum}&{date}")]
        /// <summary>
        /// 利用身分證後四碼及生日查詢會員資料
        /// </summary>
        public async Task<IActionResult> GetMemberByIDNumAndBirth(String partIDNum, String date)
        {
            try
            {
                var member = await _memberRepo.GetMemberByIDNumAndBirth(partIDNum, date);
                if (member == null) 
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "member fetched.",
                    member
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
