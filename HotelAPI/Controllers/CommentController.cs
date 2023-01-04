using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentRepository _facilityRepo;

        public CommentController(ILogger<CommentController> logger, ICommentRepository facilityRepo)
        {
            _logger = logger;
            _facilityRepo = facilityRepo;
        }

        /// <summary>
        /// 查詢所有評論資料
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            try
            {
                var comments = await _facilityRepo.GetComments();
                return Ok(new
                {
                    Success = true,
                    Message = "All comments returned.",
                    comments,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 查詢指定ID的單一評論資料
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            try
            {
                var comment = await _facilityRepo.GetComment(id);
                if (comment == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "Comment fetched.",
                    comment,
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增Comment資料
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentForCreationDto comment)
        {
            try
            {
                var createComment = await _facilityRepo.CreateComment(comment);
                return Ok(new
                {
                    Success = true,
                    Message = "Comment created.",
                    createComment,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        /// <summary>
        /// 修改指定ID的Comment資料
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment(int id, CommentForUpdateDto comment)
        {
            try
            {
                var dbComment = await _facilityRepo.GetComment(id);
                if (dbComment == null)
                    return NotFound();
                await _facilityRepo.UpdateComment(id, comment);
                return Ok(new
                {
                    Success = true,
                    Message = "Comment updated.",
                    comment,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 刪除指定ID的Comment資料
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var dbComment = await _facilityRepo.GetComment(id);
                if (dbComment == null)
                    return NotFound();
                await _facilityRepo.DeleteComment(id);
                return Ok(new
                {
                    Success = true,
                    Message = "Comment deleted."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///查詢指定Member的Mid 所在Comment資料
        /// </summary>
        [HttpGet]
        [Route("ByMemberId/{id}")]
        public async Task<IActionResult> GetCommentByMemberMId(Guid id)
        {
            try
            {
                var comment = await _facilityRepo.GetCommentByMemberMId(id);
                return Ok(new
                {
                    Success = true,
                    Message = "Comment fetched.",
                    comment,
                });
            }
            catch (Exception ex)
            { 
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///查詢所有的Comments，以及它底下的所有Members資料
        /// </summary>
        [HttpGet]
        [Route("mutiple")]
        public async Task<IActionResult> GetCommentsMembersMultipleMapping()
        {
            try
            {
                var comment = await _facilityRepo.GetCommentsMembersMultipleMapping();
                return Ok(new
                {
                    Success = true,
                    comment,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
