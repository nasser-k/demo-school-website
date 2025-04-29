using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Models;

namespace demo_school_website.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContentPage> ContentPages { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<NewsEvent> NewsEvents { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use a fixed date for seeding
            var seedDate = new DateTime(2025, 1, 1);

            // Seed initial content pages
            modelBuilder.Entity<ContentPage>().HasData(
                new ContentPage
                {
                    Id = 1,
                    Title = "Home",
                    Slug = "home",
                    Content = "<h1>Welcome to Our School</h1><p>We provide quality education for all students.</p>",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate,
                    IsPublished = true
                },
                new ContentPage
                {
                    Id = 2,
                    Title = "About Us",
                    Slug = "about",
                    Content = "<h1>About Our School</h1><p>Learn about our history, mission, and values.</p>",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate,
                    IsPublished = true
                }
            );

            // Seed initial contact info
            modelBuilder.Entity<ContactInfo>().HasData(
                new ContactInfo
                {
                    Id = 1,
                    Address = "123 School Street, City, State, ZIP",
                    Phone = "(555) 123-4567",
                    Email = "info@schoolwebsite.com",
                    MapEmbedCode = "<iframe src=\"https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3024.2219901290355!2d-74.00369368400567!3d40.71312937933185!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89c25a23e28c1191%3A0x49f75d3281df052a!2s150%20Park%20Row%2C%20New%20York%2C%20NY%2010007%2C%20USA!5e0!3m2!1sen!2sbg!4v1588812740225!5m2!1sen!2sbg\" width=\"600\" height=\"450\" frameborder=\"0\" style=\"border:0;\" allowfullscreen=\"\" aria-hidden=\"false\" tabindex=\"0\"></iframe>",
                    UpdatedAt = seedDate
                }
            );
        }
    }
}
