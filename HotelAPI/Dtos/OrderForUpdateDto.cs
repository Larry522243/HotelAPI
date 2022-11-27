using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Dtos
{
    public class OrderForUpdateDto
    {
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
