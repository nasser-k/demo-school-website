using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_school_website.Data;
using demo_school_website.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_school_website.Pages.Gallery
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>();

        public async Task OnGetAsync()
        {
            GalleryImages = await _context.GalleryImages
                .Where(g => g.IsActive)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }
    }
}
