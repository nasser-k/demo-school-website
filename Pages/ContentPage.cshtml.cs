using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System.Threading.Tasks;

namespace demo_school_website.Pages
{
    public class ContentPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ContentPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ContentPage? ContentPage { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return NotFound();
            }

            ContentPage = await _context.ContentPages
                .FirstOrDefaultAsync(m => m.Slug == slug && m.IsPublished);

            if (ContentPage == null)
            {
                return Page();
            }

            return Page();
        }
    }
}
