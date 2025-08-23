using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Interfaces.Rest.Resources;

namespace NoteTagr.Api.Notes.Interfaces.Rest.Transform;

public class NoteResourceFromEntityAssembler
{
    public static NoteResource ToResourceFromEntity(Note entity)
    {
        var tags = entity.Tags.Select(TagDTOFromEntityAssembler.ToResourceFromEntity);
            
        return new NoteResource(
            entity.Id,
            entity.UserId,
            entity.Title, 
            entity.Content, 
            entity.Archived,
            tags,
            entity.CreatedDate, 
            entity.UpdatedDate);
    }
}