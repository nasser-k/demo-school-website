using System;
using System.ComponentModel.DataAnnotations;

namespace demo_school_website.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(255)]
        public string ImagePath { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
    }
}
