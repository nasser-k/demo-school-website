using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_school_website.Areas.Admin.Pages.ContentPages
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ContentPage> ContentPages { get; set; } = new List<ContentPage>();

        public async Task OnGetAsync()
        {
            ContentPages = await _context.ContentPages
                .OrderByDescending(p => p.UpdatedAt)
                .ToListAsync();
        }
    }
}
