using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    /// <summary>
    /// 訂單細節
    /// </summary>
    public class Haves
    {
        /// <summary>
        /// 訂單細節編號
        /// </summary>
        [Key]
        public Guid Guid { get; set; }

        /// <summary>
        /// 訂單編號
        /// </summary>
        [Required]
        public string? OId { get; set; }

        /// <summary>
        /// 房間類型編號
        /// </summary>
        [Required]
        public int RTId { get; set; }

        /// <summary>
        /// 房間類型數量
        /// </summary>
        [Required]
        public int HCount { get; set; }
    }
}
