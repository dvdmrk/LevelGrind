using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MvcLoginRegistration.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Every Hero needs a name!")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="This gym only costs an Email Address...")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required(ErrorMessage ="With a password you can safely gain access to our training ground")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Please confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required for hyperbolic time chamber calibrations")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Required to enter the chamber of secrets")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Winter is coming!")]
        public int Height { get; set; }
    }
}