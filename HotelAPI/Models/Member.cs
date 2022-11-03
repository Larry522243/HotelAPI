using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Member
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public DateTime Birth { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? IDNum { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? City { get; set; }
    }
}
