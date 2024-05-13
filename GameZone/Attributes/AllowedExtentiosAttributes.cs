namespace GameZone.Attributes
{
    public class AllowedExtentiosAttributes:ValidationAttribute
    {
        private readonly string _allwoedExtentions;

        public AllowedExtentiosAttributes(string allwoedExtentions)
        {
            _allwoedExtentions = allwoedExtentions;
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extention= Path.GetExtension(file.FileName);

                var isAllowed = _allwoedExtentions.Split(',').Contains(extention, StringComparer.OrdinalIgnoreCase);

                if (!isAllowed)
                {
                    return new ValidationResult($"Only{_allwoedExtentions} are allowed!");
                
                }
            }
            return ValidationResult.Success;
        }
    }
}
