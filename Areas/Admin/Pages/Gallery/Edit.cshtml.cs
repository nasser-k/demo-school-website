using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.Gallery
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public GalleryImage GalleryImage { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var galleryImage = await _context.GalleryImages.FirstOrDefaultAsync(m => m.Id == id);
            
            if (galleryImage == null)
            {
                return NotFound();
            }
            
            GalleryImage = galleryImage;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var galleryImageToUpdate = await _context.GalleryImages.FindAsync(GalleryImage.Id);

            if (galleryImageToUpdate == null)
            {
                return NotFound();
            }

            // Update properties
            galleryImageToUpdate.Title = GalleryImage.Title;
            galleryImageToUpdate.Description = GalleryImage.Description;
            galleryImageToUpdate.IsActive = GalleryImage.IsActive;

            // Process new image if uploaded
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Delete old image if it exists
                if (!string.IsNullOrEmpty(galleryImageToUpdate.ImagePath))
                {
                    string oldFilePath = Path.Combine(_environment.WebRootPath, galleryImageToUpdate.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Save new image
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

                // Update image path
                galleryImageToUpdate.ImagePath = "/uploads/gallery/" + uniqueFileName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryImageExists(GalleryImage.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GalleryImageExists(int id)
        {
            return _context.GalleryImages.Any(e => e.Id == id);
        }
    }
}
