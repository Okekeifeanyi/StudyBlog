using Bloggie.Core.Implementations.Interface;
using Bloggie.Data.DbContextFolder;
using Bloggie.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Data.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }
        public async  Task<BlogPost> AddPostAsync(BlogPost blogPost)
        {
            await _bloggieDbContext.AddAsync(blogPost);
            await _bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeletePostAsync(Guid id)
        {
            var existingBlog = await _bloggieDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                _bloggieDbContext.Remove(existingBlog);
                await _bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            //The tags is adding the tags too while displaying using the navigation bar in the model
            return await _bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();


        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
           return await _bloggieDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdatePostAsync(BlogPost blogPost)
        {
            var existingBlog = await _bloggieDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.PageContent = blogPost.PageContent;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDtae = blogPost.PublishedDtae;
                existingBlog.Tags = blogPost.Tags;

                await _bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
