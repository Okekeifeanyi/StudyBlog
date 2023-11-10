using Bloggie.Core.Implementations.Interface;
using Bloggie.Data.DbContextFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthenticationDB _authenticationDB;

        public UserRepository(AuthenticationDB authenticationDB)
        {
            _authenticationDB = authenticationDB;
        }
        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
            var users = await _authenticationDB.Users.ToListAsync();
            //check for superadmin and dont display it
            var superAdminUser = await _authenticationDB.Users.FirstOrDefaultAsync(x =>
            x.Email == "superadmin@bloggie.com");

            if(superAdminUser is not null)
            {
                users.Remove(superAdminUser);
            }
            return users;
        }
    }
}
