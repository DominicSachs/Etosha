using System.ComponentModel.DataAnnotations;

namespace Etosha.Server.Common.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
