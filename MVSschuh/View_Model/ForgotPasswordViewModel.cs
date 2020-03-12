using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVSschuh.View_Model
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = ("Uncorect Email"))]
        public string Login { get; set; }
    }
}
