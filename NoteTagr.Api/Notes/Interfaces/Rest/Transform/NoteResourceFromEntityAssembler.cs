using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Interfaces.Rest.Resources;

namespace NoteTagr.Api.Notes.Interfaces.Rest.Transform;

public class NoteResourceFromEntityAssembler
{
    public static NoteResource ToResourceFromEntity(Note entity)
    {
        return new NoteResource(
            entity.Id,
            entity.Title, 
            entity.Content, 
            entity.Archived,
            entity.Tags,
            entity.CreatedDate, 
            entity.UpdatedDate);
    }
}