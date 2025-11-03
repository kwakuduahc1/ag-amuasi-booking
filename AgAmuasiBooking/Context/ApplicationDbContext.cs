using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AgAmuasiBooking.Models;
using Microsoft.AspNetCore.Identity;

namespace AgAmuasiBooking.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<Bookings> Bookings { get; set; }

        public virtual DbSet<BookingServices> BookingServices { get; set; }

        public virtual DbSet<ServiceCosts> ServiceCosts { get; set; }

        public virtual DbSet<Services> Services { get; set; }

        public virtual DbSet<ExtraServices> ExtraServices { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            foreach (var entity in mb.Model.GetEntityTypes())
            {
                // Replace table names
                entity.SetTableName(entity.GetTableName()?.ToLower());

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName()?.ToLower());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName()?.ToLower());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.Name?.ToLower());
                }
            }

            //mb.Entity<Bookings>(x =>
            //{
            //    x.Property(p=>p.UserName)
            //})

            // Configure Identity table column lengths
            mb.Entity<ApplicationUser>(b =>
            {
                b.HasAlternateKey(x => x.UserName);
                b.Property(u => u.UserName).HasMaxLength(70);
                b.Property(u => u.NormalizedUserName).HasMaxLength(70);
                b.Property(u => u.Email).HasMaxLength(70);
                b.Property(u => u.NormalizedEmail).HasMaxLength(70);
                b.Property(u => u.SecurityStamp).HasMaxLength(64);
                b.Property(u => u.PasswordHash).HasMaxLength(168);
                b.Property(u => u.ConcurrencyStamp).HasMaxLength(64);
            });

            mb.Entity<IdentityRole<Guid>>(b =>
            {
                b.Property(r => r.Name).HasMaxLength(70);
                b.Property(r => r.NormalizedName).HasMaxLength(70);
                b.Property(r => r.ConcurrencyStamp).HasMaxLength(64);
            });

            // Configure claim types and values
            mb.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.Property(c => c.ClaimType).HasMaxLength(100);
                b.Property(c => c.ClaimValue).HasMaxLength(100);
            });

            mb.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.Property(c => c.ClaimType).HasMaxLength(100);
                b.Property(c => c.ClaimValue).HasMaxLength(100);
            });

            // Configure UserLogin and UserToken provider/key lengths
            mb.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);
                b.Property(l => l.ProviderDisplayName).HasMaxLength(100);
            });

            mb.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.Property(t => t.LoginProvider).HasMaxLength(128);
                b.Property(t => t.Name).HasMaxLength(128);
                b.Property(t => t.Value).HasMaxLength(512);
            });

            var date = new DateTime(2025, 10, 18);
            mb.Entity<Services>(b => b.HasData(
                new Services
                {
                    ServiceName = "Catering",
                    ServicesID = 1
                },
                new Services
                {
                    ServiceName = "Decoration",
                    ServicesID = 2
                },
                new Services
                {
                    ServiceName = "Photography",
                    ServicesID = 3
                },
                new Services
                {
                    ServiceName = "Internet",
                    ServicesID = 4
                }
                ));
            mb.Entity<ServiceCosts>(b => b.HasData(
                new ServiceCosts
                {
                    ServiceCostsID = 1,
                    ServicesID = 1,
                    Cost = 500.00M,
                    DateSet = date
                },
                new ServiceCosts
                {
                    ServiceCostsID = 2,
                    ServicesID = 2,
                    Cost = 300.00M,
                    DateSet = date
                },
                new ServiceCosts
                {
                    ServiceCostsID = 3,
                    ServicesID = 3,
                    Cost = 400.00M,
                    DateSet = date
                },
                new ServiceCosts
                {
                    ServiceCostsID = 4,
                    ServicesID = 4,
                    Cost = 150.00M,
                    DateSet = date
                }
                ));
        }
    }
}
