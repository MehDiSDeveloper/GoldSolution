using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.AccountDTO
{
    public class ShowUserDTO
    {
        [Required(ErrorMessage = "UserName cant be blank")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "FirstName cant be blank")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName cant be blank")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Phone cant be blank")]
        public string? Phone { get; set; }
        public DateTime? CreateDateTime { get; set; }

        [Required(ErrorMessage = "Phone cant be blank")]
        public string? UserRoles { get; set; }
    }
}
