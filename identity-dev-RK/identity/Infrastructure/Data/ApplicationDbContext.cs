using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid,
    IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    AppToken>
    {
        public DbSet<AppToken> appTokens { get; set; }
        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<AppRole> appRoles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedData(builder);
            base.OnModelCreating(builder);
        }
        private void SeedData(ModelBuilder builder)
        {
            var adminId = Guid.Parse("1f4a577b-d3f0-458b-83f0-c12f28df4f1f");
            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                Role = Role.SuperAdmin,
                UserName = "SuperAdmin",
                NormalizedUserName = "SuperAdmin".ToUpper(),
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com".ToUpper(),
                SecurityStamp = "HNHBNE7CVLFYUOEZHQK7X74TP5F5KNBD",
                ConcurrencyStamp = "873828bc-1ca4-4779-b4f3-1f33ffe38c69"
            });


              var adminRoleId = Guid.Parse("b6cd9ff6-4778-43ea-8e9b-105f7fc5dc57");
            builder.Entity<AppRole>().HasData(
                new AppRole(Role.SuperAdmin) { Id = adminRoleId, ConcurrencyStamp = "ef0233ab-fbed-4479-8869-29c687966a42" },
                new AppRole(Role.Admin) { Id = Guid.Parse("bededc0e-4850-4d1a-b683-bf3b03d30b81"), ConcurrencyStamp = "634e9974-3746-4556-9451-a4e635b4e578" },
                new AppRole(Role.User) { Id = Guid.Parse("40df127e-e36a-4564-8cd3-231f4ed75a0a"), ConcurrencyStamp = "129e3f49-7902-4637-8454-e02f5feab143" },
                new AppRole(Role.Shelter) { Id = Guid.Parse("eb894bda-cd60-4d9f-a14a-81e0f6e864b6"), ConcurrencyStamp = "a400e99f-07fb-4df3-a0a0-3a6c55f839a7" },
                new AppRole(Role.Worker) { Id = Guid.Parse("90e5bcf9-36e8-4dd8-92ff-9e205549dd37"), ConcurrencyStamp = "40d8d2ce-a965-49b5-8283-a60809c3fd97" }
                );
                builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
                {
                UserId = adminId,
                RoleId = adminRoleId
            });
        }
    }
}
