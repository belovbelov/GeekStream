using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Никнейм")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен состоять минимум из {2} и максимум из {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароль не совпадает.")]
        public string ConfirmPassword { get; set; }
    }
}