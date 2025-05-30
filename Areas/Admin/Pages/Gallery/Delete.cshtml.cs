using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System.IO;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.Gallery
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DeleteModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public GalleryImage GalleryImage { get; set; } = default!;

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
            var galleryImage = await _context.GalleryImages.FindAsync(GalleryImage.Id);

            if (galleryImage != null)
            {
                // Delete the image file from the server
                if (!string.IsNullOrEmpty(galleryImage.ImagePath))
                {
                    string filePath = Path.Combine(_environment.WebRootPath, galleryImage.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                // Remove from database
                _context.GalleryImages.Remove(galleryImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
