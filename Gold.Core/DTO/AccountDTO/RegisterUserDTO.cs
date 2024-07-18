using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.AccountDTO
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "UserName cant be blank")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "FirstName cant be blank")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName cant be blank")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone cant be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "this is not a proper Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "password cant be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword cant be blank")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
