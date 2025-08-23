using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Queries;

namespace NoteTagr.Api.Notes.Domain.Services;

public interface ITagQueryService
{
    Task<Tag?> Handle(GetTagByIdQuery query);
    Task<IEnumerable<Tag>> Handle(GetAllTagsQuery query);
}