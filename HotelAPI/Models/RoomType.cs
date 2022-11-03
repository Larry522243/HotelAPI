using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class RoomType
    {
        [Key]
        public int RTId { get; set; }

        [Required]
        public string? RTName { get; set; }

        [Required]
        public string? RTSquare { get; set; }


    }
}
