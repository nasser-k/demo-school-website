using System;
using System.ComponentModel.DataAnnotations;

namespace demo_school_website.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(500)]
        public string? MapEmbedCode { get; set; }

        [StringLength(100)]
        public string? FacebookUrl { get; set; }

        [StringLength(100)]
        public string? TwitterUrl { get; set; }

        [StringLength(100)]
        public string? InstagramUrl { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
