using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    /// <summary>
    /// 公共設施
    /// </summary>
    public class Facility
    {
        /// <summary>
        /// 公共設施編號
        /// </summary>
        [Key]
        public int FId { get; set; }

        /// <summary>
        /// 公共設施名稱
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? FName { get; set; }

        /// <summary>
        /// 公共設施樓層
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? FFloor { get; set; }

        /// <summary>
        /// 公共設施開放時間
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? FTime { get; set; }

        /// <summary>
        /// 公共設施開放人數
        /// </summary>
        [Required]
        public int FPeople { get; set; }
    }
}
