using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Dtos
{
    public class MemberForCreationDto
    {
        /// <summary>
        /// 會員姓氏
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? FirstName { get; set; }

        /// <summary>
        /// 會員名字
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? LastName { get; set; }

        /// <summary>
        /// 會員性別
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Gender { get; set; }

        /// <summary>
        /// 會員生日
        /// </summary>
        [Required]
        public DateTime Birth { get; set; }

        /// <summary>
        /// 會員行動電話
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Phone { get; set; }

        /// <summary>
        /// 會員信箱
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string? Email { get; set; }

        /// <summary>
        /// 會員密碼
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Password { get; set; }

        /// <summary>
        /// 會員身分證字號
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? IDNum { get; set; }

        /// <summary>
        /// 會員居住國家
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Country { get; set; }

        /// <summary>
        /// 會員居住城市
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? City { get; set; }
    }
}
