using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Queries;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Notes.Domain.Services;

namespace NoteTagr.Api.Notes.Application.Internal.QueryServices;

public class NoteQueryService(INoteRepository noteRepository) : INoteQueryService
{
    public async Task<Note?> Handle(GetNoteByIdQuery query)
    {
        return await noteRepository.FindByIdAsync(query.NoteId);
    }

    public async Task<IEnumerable<Note>> Handle(GetAllNotesQuery query)
    {
        return await noteRepository.ListAsync();
    }
}