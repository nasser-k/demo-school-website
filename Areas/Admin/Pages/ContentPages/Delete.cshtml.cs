using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.ContentPages
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContentPage ContentPage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var contentPage = await _context.ContentPages.FirstOrDefaultAsync(m => m.Id == id);

            if (contentPage == null)
            {
                return NotFound();
            }

            ContentPage = contentPage;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var contentPage = await _context.ContentPages.FindAsync(ContentPage.Id);

            if (contentPage != null)
            {
                _context.ContentPages.Remove(contentPage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
