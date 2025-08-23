using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Commands;

namespace NoteTagr.Api.Notes.Domain.Services;

public interface INoteCommandService
{
    Task<Note?> Handle(CreateNoteCommand command);
    Task<bool> Handle(DeleteNoteByIdCommand command);
}