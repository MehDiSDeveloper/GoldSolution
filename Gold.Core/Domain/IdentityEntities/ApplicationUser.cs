using Gold.Core.Domain.Entities.Ticket;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Gold.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatDateTime { get; set; } = DateTime.Now;

        //nav props
        public ICollection<ApplicationUserClaim>? Claims { get; set; }
        public ICollection<ApplicationUserLogin>? Logins { get; set; }
        public ICollection<ApplicationUserToken>? Tokens { get; set; }
        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
