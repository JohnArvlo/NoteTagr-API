using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Commands;

namespace NoteTagr.Api.Notes.Domain.Services;

public interface ITagCommandService
{
    Task<Tag?> Handle(CreateTagCommand command);
    Task<bool> Handle(DeleteTagByIdCommand command);
}