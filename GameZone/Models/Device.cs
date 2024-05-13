
namespace GameZone.Models
{
    public class Device:BaseEntitiy
    {
        [MaxLength(50)]
        public string? Icon { get; set; }

    }
}
