using System.ComponentModel.DataAnnotations;


namespace HotelAPI.Dtos
{
    public class OrderDetailsForCreationDto
    {

        /// <summary>
        /// 訂單編號
        /// </summary>
        [Key]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? OId { get; set; }

        /// <summary>
        /// 客房編號
        /// </summary>
        [Key]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RId { get; set; }
    }
}
