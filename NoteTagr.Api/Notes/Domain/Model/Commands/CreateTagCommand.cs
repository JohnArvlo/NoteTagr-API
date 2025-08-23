namespace NoteTagr.Api.Notes.Domain.Model.Commands;

public record CreateTagCommand(string Title, string? Description, int UserId);