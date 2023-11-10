using Bloggie.Model.Models;

namespace Bloggie.Core.Implementations.Interface
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>>GetAllAsync();
        Task<Tag?> GetByIdAsync(Guid id);
        Task<Tag>AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);

    }
}

