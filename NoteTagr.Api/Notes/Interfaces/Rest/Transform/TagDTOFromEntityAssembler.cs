using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Interfaces.Rest.Resources;

namespace NoteTagr.Api.Notes.Interfaces.Rest.Transform;

public class TagDTOFromEntityAssembler
{
    public static TagDTO ToResourceFromEntity(Tag entity)
    {
        return new TagDTO(
            entity.Id,
            entity.Title
        );
    }
}