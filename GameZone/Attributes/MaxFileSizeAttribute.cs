namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxfilesize;

        public MaxFileSizeAttribute(int maxfilesize)
        {
            _maxfilesize = maxfilesize;
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
               
                if (file.Length>_maxfilesize)
                {
                    return new ValidationResult($"Maximum allowed size is{_maxfilesize} bytes");

                }
            }
            return ValidationResult.Success;
        }
    }
}
