using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Crud.Models
{
    public class Product
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Desc { get; set; }

        [Required]
        [Range(0, int.MaxValue)] 
        public int Stock { get; set; }

        [Required]
        [Range(1, int.MaxValue)] 
        public int Price { get; set; }

        public byte[] Image { get; set; }

        [MaxFileSize(2 * 1024 * 1024)] 
        [AllowedExtensions(".jpg", ".jpeg", ".png")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
    // Custom Validation Attribute for MaxFileSize
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxSize;

        public MaxFileSizeAttribute(long maxSize)
        {
            _maxSize = maxSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var file = value as IFormFile;
            if (file != null && file.Length > _maxSize)
            {
                return new ValidationResult($"File size exceeds the maximum allowed size of {_maxSize} bytes.");
            }

            return ValidationResult.Success;
        }
    }

    // Custom Validation Attribute for AllowedExtensions
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(params string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!(_allowedExtensions.Contains(extension)))
                {
                    return new ValidationResult($"File extension '{extension}' is not allowed. Only {string.Join(", ", _allowedExtensions)} extensions are allowed.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
