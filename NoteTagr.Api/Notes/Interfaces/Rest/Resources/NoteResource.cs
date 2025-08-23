using NoteTagr.Api.Notes.Domain.Model.Aggregates;

namespace NoteTagr.Api.Notes.Interfaces.Rest.Resources;

public record NoteResource(int Id, string Title, string Content, bool Archived, IEnumerable<TagDTO> Tags, DateTimeOffset? CreatedDate, DateTimeOffset? UpdatedDate);