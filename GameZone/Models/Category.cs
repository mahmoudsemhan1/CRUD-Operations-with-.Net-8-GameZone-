namespace GameZone.Models
{
    public class Category:BaseEntitiy
    {
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
