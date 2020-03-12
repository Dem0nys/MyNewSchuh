using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVSschuh.View_Model
{
    public class ChangePassword
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,24}$", ErrorMessage = ("Uncorect Password"))]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don`t match")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        public string PasswordConfirm { get; set; }
    }
}
