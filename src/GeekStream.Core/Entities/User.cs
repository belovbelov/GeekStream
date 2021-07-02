using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.Entities
{
    public class User
    {
       public int Id { get; set; }

       [Required]
       [StringLength(32, MinimumLength = 3)]
       [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

    }
}