using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    /// <summary>
    /// 餐廳
    /// </summary>
    public class Restaurant
    {
        /// <summary>
        /// 餐廳編號
        /// </summary>
        [Key]
        public int REId { get; set; }

        /// <summary>
        /// 餐廳名稱
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? REName { get; set; }

        /// <summary>
        /// 餐廳樓層
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? REFloor { get; set; }

        /// <summary>
        /// 餐廳總座位數
        /// </summary>
        [Required]
        public int RESeat { get; set; }

        /// <summary>
        /// 餐廳開放時間
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RETime { get; set; }

        /// <summary>
        /// 餐廳類型
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? REType { get; set; }


    }
}
