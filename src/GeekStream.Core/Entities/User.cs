using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekStream.Core.Entities
{
    public class User
    {
        public User()
        {
            
        }

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

        public ICollection<Article> AuthoredArticles { get; set; }
    }
}