using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Domain.IdentityEntities
{
    public class ApplicationRole:IdentityRole<string>
    {
        
        public string? Description { get; set; }


        //nav props
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim>? RoleClaims { get; set; }

        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
