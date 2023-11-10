using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Data.DbContextFolder
{
    public class AuthenticationDB : IdentityDbContext
    {
        public AuthenticationDB(DbContextOptions<AuthenticationDB> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seeding the roles, User, Admin and Super Admin Roles
            var adminRoleID = "419d8639-4a36-481d-81c8-1a7869bd1c52";
            var superAdminRoleID = "002c28e4-8ae0-4b45-af70-a161dd3b1f4b";
            var userRoleId = "5c1b87ff-bb05-4f79-a12c-38d31600ba54";

            var roles = new List<IdentityRole>
            {
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "Admin",
                        Id = adminRoleID,
                        ConcurrencyStamp = adminRoleID

                    },

                     new IdentityRole
                    {
                        Name = "SuperAdmin",
                        NormalizedName = "SuperAdmin",
                        Id = superAdminRoleID,
                        ConcurrencyStamp = superAdminRoleID

                    },
                         new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "User",
                        Id = userRoleId,
                        ConcurrencyStamp = userRoleId

                    },
            };
                
            builder.Entity<IdentityRole>().HasData(roles);

            //Seeding Super Admin User
          
            var superAdminId = "c9ba5c89-d298-4544-ab78-c3acc4b2eab4 ";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToString(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId

            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All the roles to superAdmin User
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleID,
                    UserId = superAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleID,
                    UserId = superAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
