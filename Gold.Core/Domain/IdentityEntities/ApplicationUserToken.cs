using Gold.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;

public class ApplicationUserToken : IdentityUserToken<string>
{
    public virtual ApplicationUser User { get; set; }
}
