using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.Staff
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
        public Models.Staff Staff { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(m => m.Id == id);
            
            if (staff == null)
            {
                return NotFound();
            }
            
            Staff = staff;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var staffToUpdate = await _context.Staff.FindAsync(Staff.Id);

            if (staffToUpdate == null)
            {
                return NotFound();
            }

            // Update properties
            staffToUpdate.Name = Staff.Name;
            staffToUpdate.Position = Staff.Position;
            staffToUpdate.Bio = Staff.Bio;
            staffToUpdate.Email = Staff.Email;
            staffToUpdate.IsActive = Staff.IsActive;

            // Process new image if uploaded
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Delete old image if it exists
                if (!string.IsNullOrEmpty(staffToUpdate.ImagePath))
                {
                    string oldFilePath = Path.Combine(_environment.WebRootPath, staffToUpdate.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Save new image
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "staff");
                
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
                staffToUpdate.ImagePath = "/uploads/staff/" + uniqueFileName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(Staff.Id))
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

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
