using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using demo_school_website.Data;
using demo_school_website.Models;
using System;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.ContentPages
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContentPage ContentPage { get; set; } = new ContentPage();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ContentPage.CreatedAt = DateTime.UtcNow;
            ContentPage.UpdatedAt = DateTime.UtcNow;

            _context.ContentPages.Add(ContentPage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
