using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Queries;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Notes.Domain.Services;

namespace NoteTagr.Api.Notes.Application.Internal.QueryServices;

public class TagQueryService(ITagRepository tagRepository) : ITagQueryService
{
    public async Task<Tag?> Handle(GetTagByIdQuery query)
    {
        return await tagRepository.FindByIdAsync(query.TagId);
    }

    public async Task<IEnumerable<Tag>> Handle(GetAllTagsQuery query)
    {
        return await tagRepository.ListAsync();
    }
    
    public async Task<IEnumerable<Tag>> Handle(GetAllTagsByUserIdQuery query)
    {
        return await tagRepository.ListByUserIdAsync(query.UserId);
    }
}