
namespace GameZone.Services
{
    public class DeviceServices : IDeviceServices
    {
        private readonly ApplicationDbContext _context;
        public DeviceServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetDevices()
        {
            var Dev = _context.Devices.
                Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .ToList();
            return Dev;
        }
    }
}
