using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using System.IO;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.Staff
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
        public Models.Staff Staff { get; set; } = default!;

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
            var staff = await _context.Staff.FindAsync(Staff.Id);

            if (staff != null)
            {
                // Delete the image file from the server
                if (!string.IsNullOrEmpty(staff.ImagePath))
                {
                    string filePath = Path.Combine(_environment.WebRootPath, staff.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                // Remove from database
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
