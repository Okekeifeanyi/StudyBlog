using Bloggie.Core.Implementations.Interface;
using Bloggie.Data.DbContextFolder;
using Bloggie.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Data.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _bloggieDbContext.BlogPostLike.AddAsync(blogPostLike);
            await _bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPstId)
        {
            return await _bloggieDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPstId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await _bloggieDbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
