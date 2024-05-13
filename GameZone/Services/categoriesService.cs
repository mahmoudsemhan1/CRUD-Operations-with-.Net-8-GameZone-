
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class categoriesService : IcategoriesService
    {
        private readonly ApplicationDbContext _context;

        public categoriesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetCategories()
        {
            var cat= _context.Categories.
                Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
            return cat;
        }
       
    }
}
