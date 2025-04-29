using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.ContentPages
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ContentPage.UpdatedAt = DateTime.UtcNow;

            _context.Attach(ContentPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentPageExists(ContentPage.Id))
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

        private bool ContentPageExists(int id)
        {
            return _context.ContentPages.Any(e => e.Id == id);
        }
    }
}
