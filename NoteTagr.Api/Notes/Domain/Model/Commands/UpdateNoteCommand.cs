namespace NoteTagr.Api.Notes.Domain.Model.Commands;

public record UpdateNoteCommand(int NoteId, string Title, string Content, bool Archived);