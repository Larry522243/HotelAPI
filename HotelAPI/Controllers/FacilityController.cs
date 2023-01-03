using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly ILogger<FacilityController> _logger;
        private readonly IFacilitiesRepository _facilityRepo;

        public FacilityController(ILogger<FacilityController> logger, IFacilitiesRepository facilityRepo)
        {
            _logger = logger;
            _facilityRepo = facilityRepo;
        }

        /// <summary>
        /// 查詢所有設施資料
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetFacilities()
        {
            try
            {
                var facilities = await _facilityRepo.GetFacilities();
                return Ok(new
                {
                    Success = true,
                    Message = "All facilities returned.",
                    facilities
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 查詢指定ID的單一設施資料
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFacility(Int16 id)
        {
            try
            {
                var facility = await _facilityRepo.GetFacility(id);
                if (facility == null)
                    return NotFound();
                return Ok(new
                {
                    Success = true,
                    Message = "Facility fetched.",
                    facility
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增Facility資料
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateFacility(FacilitiesForCreationDto facility)
        {
            try
            {
                var createdFacility = await _facilityRepo.CreateFacility(facility);
                return Ok(new
                {
                    Success = true,
                    Message = "Facility Created.",
                    createdFacility,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 修改指定ID的Facility資料
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFacility(Int16 id, FacilitiesForUpdateDto facility)
        {
            try
            {
                var dbFacility = await _facilityRepo.GetFacility(id);
                if (dbFacility == null)
                    return NotFound();
                await _facilityRepo.UpdateFacility(id, facility);
                return Ok(new
                {
                    Success = true,
                    Message = "Facility Updated.",
                    facility,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
