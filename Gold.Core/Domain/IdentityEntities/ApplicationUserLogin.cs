using Gold.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;

public class ApplicationUserLogin : IdentityUserLogin<string>
{
    public virtual ApplicationUser User { get; set; }
}
