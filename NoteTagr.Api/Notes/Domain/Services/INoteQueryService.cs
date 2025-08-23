using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Queries;

namespace NoteTagr.Api.Notes.Domain.Services;

public interface INoteQueryService
{
    Task<Note?> Handle(GetNoteByIdQuery query);
    Task<IEnumerable<Note>> Handle(GetAllNotesQuery query);
    Task<IEnumerable<Note>> Handle(GetAllNotesByUserIdQuery query);
    
}