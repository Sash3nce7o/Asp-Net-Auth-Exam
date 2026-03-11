using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Exam.Core.Models.Auth
{
    public class LoginFormViewModel
    {
        [Required (ErrorMessage = "RequiredField")]
        [EmailAddress (ErrorMessage = "InvalidEmailFormat")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage ="RequiredField")]
        public string Password { get; set; } = string.Empty;
        
        
        
    }
}