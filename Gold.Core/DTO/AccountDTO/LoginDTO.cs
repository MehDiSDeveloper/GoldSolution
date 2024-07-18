using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.AccountDTO
{
    public class LoginDTO
    {
        
        [Required(ErrorMessage = "Phone cant be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "this is not a proper Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "password cant be blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
   
    }
}
