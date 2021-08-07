using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электроной почты")]
        public string Email { get; set; }
    }
}