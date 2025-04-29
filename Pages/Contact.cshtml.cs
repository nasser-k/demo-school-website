using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System.Threading.Tasks;

namespace demo_school_website.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ContactModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ContactInfo ContactInfo { get; set; } = new ContactInfo();

        [TempData]
        public string? SuccessMessage { get; set; }

        public async Task OnGetAsync()
        {
            ContactInfo = await _context.ContactInfo.FirstOrDefaultAsync() ?? new ContactInfo();
        }

        public async Task<IActionResult> OnPostAsync(string name, string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || 
                string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
            {
                return Page();
            }

            // In a real application, you would send an email here
            // For now, we'll just set a success message
            SuccessMessage = "Thank you for your message. We will get back to you soon!";

            ContactInfo = await _context.ContactInfo.FirstOrDefaultAsync() ?? new ContactInfo();
            
            return Page();
        }
    }
}
