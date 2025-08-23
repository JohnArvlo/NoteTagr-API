namespace NoteTagr.Api.Notes.Interfaces.Rest.Resources;

public record UpdateNoteResource(string Title, string Content, bool Archived);