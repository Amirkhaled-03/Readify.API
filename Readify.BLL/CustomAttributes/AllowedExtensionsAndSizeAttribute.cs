using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.CustomAttributes
{
    public class AllowedExtensionsAndSizeAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly int _maxFileSizeInMB;

        public AllowedExtensionsAndSizeAttribute(string[] extensions, int maxFileSizeInMB)
        {
            _extensions = extensions;
            _maxFileSizeInMB = maxFileSizeInMB;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
                return new ValidationResult("هذا الحقل مطلوب");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_extensions.Contains(extension))
                return new ValidationResult($"يسمح فقط بالصور من نوع {string.Join(", ", _extensions)}");

            if (file.Length > _maxFileSizeInMB * 1024 * 1024)
                return new ValidationResult($"الملف يجب ألا يتجاوز {_maxFileSizeInMB} ميجابايت");

            return ValidationResult.Success;
        }
    }
}
