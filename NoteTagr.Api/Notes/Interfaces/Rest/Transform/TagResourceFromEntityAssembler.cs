using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Interfaces.Rest.Resources;

namespace NoteTagr.Api.Notes.Interfaces.Rest.Transform;

public class TagResourceFromEntityAssembler
{
    public static TagResource ToResourceFromEntity(Tag entity)
    {
        return new TagResource(
            entity.Id,
            entity.UserId,
            entity.Title,
            entity.Description,
            // entity.Notes,
            entity.CreatedDate,
            entity.UpdatedDate
        );
    }
}