using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Dtos
{
    public class OrderForCreationDto
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        [Key]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? OId { get; set; }

        /// <summary>
        /// 會員編號
        /// </summary>
        [Required]
        public Guid MId { get; set; }

        /// <summary>
        /// 訂單建立日期
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 入房日期
        /// </summary>
        [Required]
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// 退房日期
        /// </summary>
        [Required]
        public DateTime CheckOut { get; set; }

        /// <summary>
        /// 訂單總房數
        /// </summary>
        [Required]
        public int ORoomCnt { get; set; }

        /// <summary>
        /// 訂單總人數
        /// </summary>
        [Required]
        public int OPeopleCnt { get; set; }
    }
}
