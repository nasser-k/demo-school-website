using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_school_website.Pages.Staff
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Staff> StaffList { get; set; } = new List<Models.Staff>();

        public async Task OnGetAsync()
        {
            StaffList = await _context.Staff
                .Where(s => s.IsActive)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
    }
}
