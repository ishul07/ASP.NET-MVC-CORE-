using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"((?=.*[A-Za-z0-9])(?=.*[@#$%!^&*()+?]).{6,})", ErrorMessage = "Must be atleast 5 characters and contains 1 special characters")]
        public string Password { get; set; }
    }
}