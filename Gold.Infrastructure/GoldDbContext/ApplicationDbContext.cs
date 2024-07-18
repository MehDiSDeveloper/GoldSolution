
using Gold.Core.Domain.Entities.Assets;
using Gold.Core.Domain.Entities.Finance;
using Gold.Core.Domain.Entities.Product;
using Gold.Core.Domain.Entities.Ticket;
using Gold.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gold.Infrastructure.GoldDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<GoldPrice> GoldPrices { get; set; }
        public virtual DbSet<UserGoldAsset> UserGoldAssets { get; set; }
        public virtual DbSet<UserCashToGold> UserCashGolds { get; set; }
        public virtual DbSet<UserGoldToCash> UserGoldCashs { get; set; }
        public virtual DbSet<SystemCashToGold> SystemCashToGolds { get; set; }
        public virtual DbSet<SystemGoldToCash> SystemGoldToCashs { get; set; }
        public virtual DbSet<UserSystemGoldToCashBill> UserSystemGoldToCashBills { get; set; }
        public virtual DbSet<UserSystemCashToGoldBill> UserSystemCashToGoldBills { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketMessage> TicketMessages { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }





        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=GoldDb; Integrated Security=True;TrustServerCertificate=True;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Price>().HasKey(t => new { t.priceId });

            var hasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser user = new()
            {
                Id = "8c72dff883b3403e81dc7bf88ce26b8a",
                UserName = "MainAdmin",
                FirstName = "Mahdi",
                LastName = "Montazeri",
                PhoneNumber = "09125850371",
                PasswordHash = hasher.HashPassword(null, "56181Roya@"),
                SecurityStamp = "1",
                CreatDateTime = DateTime.Now,
            };


            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>() { RoleId = role.Id.ToString(), UserId = user.Id.ToString() }
            //    );

            ///
            //PasswordHasher<ApplicationUser> passwordHasher = new();
            //passwordHasher.HashPassword(user, "56181Roya@");
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.HasData(user);

                b.HasIndex(u => u.PhoneNumber).IsUnique(true);
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.HasData(
                new ApplicationRole()
                {
                    Id = "ad6151ed16984999b3ff121c5c0bfd96",
                    Name = "MainAdmin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "MainAdmin"
                });
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });


            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.HasKey(IUR => new { IUR.RoleId, IUR.UserId });

                b.HasData(new ApplicationUserRole()
                {
                    RoleId = "ad6151ed16984999b3ff121c5c0bfd96",
                    UserId = "8c72dff883b3403e81dc7bf88ce26b8a"
                });
            });



            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Name = "Gold"
            });
            modelBuilder.Entity<GoldPrice>().HasData(new GoldPrice()
            {
                Price = 105000
            });
        }
    }
}
