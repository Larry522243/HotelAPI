using HotelAPI.Contracts;
using HotelAPI.Dtos;
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

        [HttpGet]
        [Route("{mid}")]
        public async Task<IActionResult> GetMember(Guid mid)
        {
            try
            {
                var member = await _memberRepo.GetMember(mid);
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
                    createdMember
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
