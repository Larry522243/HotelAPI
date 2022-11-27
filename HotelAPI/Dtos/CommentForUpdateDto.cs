using System.ComponentModel.DataAnnotations;


namespace HotelAPI.Dtos
{
    public class CommentForUpdateDto
    {
        /// <summary>
        /// 評論建立日期
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 評論星數
        /// </summary>
        [Required]
        public int Star { get; set; }

        /// <summary>
        /// 評論內容
        /// </summary>
        [Required]
        [StringLength(1000, ErrorMessage = "Maximum 1000 characters")]
        public string? Content { get; set; }
    }
}
