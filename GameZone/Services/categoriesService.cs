
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
                .ToList();
            return cat;
        }
        public IEnumerable<SelectListItem> GetDevices()
        {
            var dev = _context.Devices.
                Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .ToList();
            return dev;
        }
    }
}
