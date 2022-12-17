using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Dtos
{
    public class FacilitiesForUpdateDto
    {
       
       

        /// <summary>
        /// 開放時間
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? FTime { get; set; }

        /// <summary>
        /// 設施可容納人數
        /// </summary>
        [Required]
        public int FPeople { get; set; }


    }
}
