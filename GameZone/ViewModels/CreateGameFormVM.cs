using GameZone.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class CreateGameFormVM:GameFormVM
    {
        
        [AllowedExtentiosAttributes(FilesSettings.AllowedExtentions),
            MaxFileSize(FilesSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;

    }
}
