using System;
using System.ComponentModel.DataAnnotations;

namespace demo_school_website.Models
{
    public class NewsEvent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [StringLength(255)]
        public string? ImagePath { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsPublished { get; set; } = true;

        // Type can be "News" or "Event"
        [StringLength(20)]
        public string Type { get; set; } = "News";
    }
}
