using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Netblaster.Hermes.WebUI.Models.Authorize
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        public string UserName => $"{FirstName} {LastName}";

        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public DateTime CreateDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        [Display(Name = "Stanowisko")]
        public string JobTitle { get; set; }

        [Display(Name = "Awatar")]
        public byte[] Photo { get; set; }

    }
}