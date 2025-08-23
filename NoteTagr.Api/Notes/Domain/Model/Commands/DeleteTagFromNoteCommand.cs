namespace NoteTagr.Api.Notes.Domain.Model.Commands;

public record DeleteTagFromNoteCommand(int NoteId, int TagId);