using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    /// <summary>
    /// 房間
    /// </summary>
    public class Room
    {
        /// <summary>
        /// 房號
        /// </summary>
        [Key]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RoomId { get; set; }

        /// <summary>
        /// 房間類型編號
        /// </summary>
        [Required]
        public int RTId { get; set; }
    }
}
