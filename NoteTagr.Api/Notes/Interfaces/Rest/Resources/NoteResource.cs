namespace NoteTagr.Api.Notes.Interfaces.Rest.Resources;

public record NoteResource(string Title, string Content, bool Archived, DateTimeOffset? CreatedDate, DateTimeOffset? UpdatedDate);