using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Commands;

namespace NoteTagr.Api.Notes.Domain.Services;

public interface INoteCommandService
{
    Task<Note?> Handle(CreateNoteCommand command);
    Task<bool> Handle(DeleteNoteByIdCommand command);
    Task<Note?> Handle(UpdateNoteCommand command);
    
    // Task <Note?> Handle(AddTagsToNoteCommand command);
    Task <Note?> Handle(int noteId, int tagId);
    
    //delete tag 
    Task<Note?> Handle(DeleteTagFromNoteCommand command);
}
