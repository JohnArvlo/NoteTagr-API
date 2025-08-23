using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Shared.Domain.Repositories;

namespace NoteTagr.Api.Notes.Domain.Repositories;

public interface ITagRepository : IBaseRepository<Tag>
{
    Task<IEnumerable<Tag>> ListByUserIdAsync(int userId);
}