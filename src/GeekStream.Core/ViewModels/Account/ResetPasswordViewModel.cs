using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Придумайте пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль еще раз")]
        [Compare("Password", 
            ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}