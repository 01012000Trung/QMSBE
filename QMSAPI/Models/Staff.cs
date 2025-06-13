using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMSAPI.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(100)]
        public string SRole { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        public string? SPassword { get; set; }

        [Required, StringLength(20)]
        public string Access { get; set; }

        [StringLength(255)]
        public string? SAddress { get; set; }
    }
}
