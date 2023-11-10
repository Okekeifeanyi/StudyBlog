using Bloggie.Model.Models;

namespace Bloggie.Core.Implementations.Interface
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetByIdAsync(Guid id);

        Task<BlogPost> AddPostAsync (BlogPost blogPost);
        Task<BlogPost?> UpdatePostAsync (BlogPost blogPost);
        Task<BlogPost?> DeletePostAsync (Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);

    }
}
