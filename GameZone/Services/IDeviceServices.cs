namespace GameZone.Services
{
    public interface IDeviceServices
    {
        IEnumerable<SelectListItem> GetDevices();
    }
}
