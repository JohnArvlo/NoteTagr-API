using NoteTagr.Api.IAM.Domain.Model.Aggregates;
using NoteTagr.Api.IAM.Interfaces.REST.Resources;

namespace NoteTagr.Api.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
    
}