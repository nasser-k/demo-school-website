using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.Staff
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
