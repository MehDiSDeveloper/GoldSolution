using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.AccountDTO
{
    public class EditUserDTO
    {
        [Required]
        public string? Id { get; set; }
        [Required(ErrorMessage = "UserName cant be blank")]

        public string? UserName { get; set; }

        [Required(ErrorMessage = "FirstName cant be blank")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName cant be blank")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Phone cant be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "this is not a proper Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
}
