using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    /// <summary>
    /// 房間類型
    /// </summary>
    public class RoomType
    {
        /// <summary>
        /// 房間類型編號
        /// </summary>
        [Key]
        public int RTId { get; set; }

        /// <summary>
        /// 房間類型名稱
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RTName { get; set; }

        /// <summary>
        /// 房間類型坪數
        /// </summary>
        [Required]
        public float RTSquare { get; set; }

        /// <summary>
        /// 房間類型床數
        /// </summary>
        [Required]
        public int RTBed { get; set; }

        /// <summary>
        /// 房間類型可住宿人數
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public int? RTPeople { get; set; }

        /// <summary>
        /// 房間類型所在樓層
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RTFloor { get; set; }

        /// <summary>
        /// 房間類型價格
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public int RTPrice { get; set; }

        /// <summary>
        /// 房間類型備註
        /// </summary>
        [StringLength(200, ErrorMessage = "Maximum 200 characters")]
        public string? RTMark { get; set; }
    }
}
