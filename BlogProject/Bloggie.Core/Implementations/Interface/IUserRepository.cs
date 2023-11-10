using Microsoft.AspNetCore.Identity;

namespace Bloggie.Core.Implementations.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsers();
    }
}
