using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Shared.Domain.Repositories;

namespace NoteTagr.Api.Notes.Domain.Repositories;

public interface INoteRepository : IBaseRepository<Note>
{
    Task<Note?> GetNoteWithTagsAsync(int id);
    Task<IEnumerable<Note>> ListWithTagsAsync();
    Task<IEnumerable<Note>> ListByUserIdWithTagsAsync(int userId);
}