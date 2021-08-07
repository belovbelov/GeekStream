using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}