using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class CreateUser
    {

        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Both first and last name is required with single space between them(only alphabets are allowed)")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"((?=.*[A-Za-z0-9])(?=.*[@#$%!^&*()+?]).{6,})", ErrorMessage = "Must be atleast 5 characters and contains 1 special characters")]
        public string Password { get; set; }
    }
}