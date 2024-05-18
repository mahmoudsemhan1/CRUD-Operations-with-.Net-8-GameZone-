using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class EditGameFormVM:GameFormVM
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }

        [AllowedExtentiosAttributes(FilesSettings.AllowedExtentions),
            MaxFileSize(FilesSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
