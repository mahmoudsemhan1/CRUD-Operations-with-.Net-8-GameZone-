namespace GameZone.Services
{
    public interface IcategoriesService
    {
        IEnumerable<SelectListItem> GetCategories();

    }
}
