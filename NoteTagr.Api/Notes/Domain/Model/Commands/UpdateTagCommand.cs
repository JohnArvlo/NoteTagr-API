namespace NoteTagr.Api.Notes.Domain.Model.Commands;

public record UpdateTagCommand(string Title, String? Description);