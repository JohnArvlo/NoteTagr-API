using NoteTagr.Api.Notes.Domain.Model.Aggregates;

namespace NoteTagr.Api.Notes.Interfaces.Rest.Resources;

public record TagResource(int Id, string Name, string? Description, /*ICollection<Note> Notes,*/ DateTimeOffset? CreatedDate, DateTimeOffset? UpdatedDate);