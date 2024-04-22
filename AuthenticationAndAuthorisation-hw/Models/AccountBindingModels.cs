using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorization_hw.Models
{
    public class RegisterBindingModel
    {
        [Required]
        public string Login {  get; set; }

        [Required]
        [UIHint("Password")]
        public string Password {  get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm password")]
        [UIHint("Password")]
        public string PasswordConfirm { get; set; }
    }


    public class AuthorizeBindingModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [UIHint("Password")]
        public string Password { get; set; }
    }
}
