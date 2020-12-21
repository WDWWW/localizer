using System.ComponentModel.DataAnnotations;

namespace Localizer.Domain.Entities
{
    public class User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Name { get; set; }
    }
}