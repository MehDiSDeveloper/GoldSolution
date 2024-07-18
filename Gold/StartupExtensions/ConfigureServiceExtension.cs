using Gold.Core.Domain.IdentityEntities;
using Gold.Core.ServiceContracts;
using Gold.Infrastructure.Services;
using Gold.Infrastructure.GoldDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gold.UI
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddSignalR();


            //add services here
            services.AddDbContext<ApplicationDbContext>(Options => {
                Options.UseSqlServer(configuration.GetConnectionString("LocalConnection"));
            });

            //add services here
            services.AddScoped<IAccountServices,AccountServices>();
            services.AddScoped<IRoleServices,RoleServices>();
            services.AddScoped<IGoldServices, GoldServices>();
            services.AddScoped<IPayServices, PayServices>();
            services.AddScoped<ITicketServices, TicketServices>();

            services.AddIdentity<ApplicationUser,ApplicationRole>(options=>
            {
                options.Password.RequiredUniqueChars = 0;
            }
            )
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser,ApplicationRole,ApplicationDbContext,string,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin,ApplicationUserToken,ApplicationRoleClaim>>()
            .AddRoleStore<RoleStore<ApplicationRole,ApplicationDbContext,string,ApplicationUserRole,ApplicationRoleClaim>>();

            return services;
        }

    }
}
