using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Dtos
{
    public class RoomForCreationDto
    {
        /// <summary>
        /// 客房編號
        /// </summary>
        [Key]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RId { get; set; }

        /// <summary>
        /// 客房名稱
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RName { get; set; }

        /// <summary>
        /// 客房坪數
        /// </summary>
        [Required]
        public float RSquare { get; set; }

        /// <summary>
        /// 客房床數
        /// </summary>
        [Required]
        public int RBed { get; set; }

        /// <summary>
        /// 客房可容納人數
        /// </summary>
        [Required]
        public int RPeople { get; set; }

        /// <summary>
        /// 客房樓層
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RFloor { get; set; }

        /// <summary>
        /// 客房定價
        /// </summary>
        [Required]
        public int RPrice { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Required]
        [StringLength(200, ErrorMessage = "Maximum 200 characters")]
        public string? RMark { get; set; }
    }
}
