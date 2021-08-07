using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}