using System.ComponentModel.DataAnnotations;


namespace HotelAPI.Dtos
{
    public class OrderDetailForUpdateDto
    {
        /// <summary>
        /// 客房編號
        /// </summary>
        [Key]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? RId { get; set; }
    }
}
