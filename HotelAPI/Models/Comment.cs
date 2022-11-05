﻿using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    /// <summary>
    /// 評論
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 評論編號
        /// </summary>
        [Required]
        public int CId { get; set; }

        /// <summary>
        /// 會員編號
        /// </summary>
        [Required]
        public Guid MId { get; set; }

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