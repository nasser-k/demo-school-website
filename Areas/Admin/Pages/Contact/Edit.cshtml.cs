using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.Contact
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
        public ContactInfo ContactInfo { get; set; } = default!;

        [TempData]
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get the first contact info record or create a new one if none exists
            ContactInfo = await _context.ContactInfo.FirstOrDefaultAsync() ?? new ContactInfo();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ContactInfo.UpdatedAt = DateTime.UtcNow;

            // Check if we're updating an existing record or creating a new one
            if (ContactInfo.Id == 0)
            {
                _context.ContactInfo.Add(ContactInfo);
            }
            else
            {
                _context.Attach(ContactInfo).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
                SuccessMessage = "Contact information updated successfully.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactInfoExists(ContactInfo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit");
        }

        private bool ContactInfoExists(int id)
        {
            return _context.ContactInfo.Any(e => e.Id == id);
        }
    }
}
