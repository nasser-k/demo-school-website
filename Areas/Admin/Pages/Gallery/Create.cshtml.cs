using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using demo_school_website.Data;
using demo_school_website.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.Gallery
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public GalleryImage GalleryImage { get; set; } = new GalleryImage();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select an image file.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Process and save the uploaded image
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "gallery");
            
            // Create directory if it doesn't exist
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate a unique filename
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }

            // Set the image path in the model
            GalleryImage.ImagePath = "/uploads/gallery/" + uniqueFileName;
            GalleryImage.CreatedAt = DateTime.UtcNow;

            _context.GalleryImages.Add(GalleryImage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
