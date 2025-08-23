namespace NoteTagr.Api.Notes.Domain.Model.Commands;

public record UpdateTagCommand(int TagId, string Title, String? Description);