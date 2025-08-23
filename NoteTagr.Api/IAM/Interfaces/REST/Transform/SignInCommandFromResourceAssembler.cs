using NoteTagr.Api.IAM.Domain.Model.Commands;
using NoteTagr.Api.IAM.Interfaces.REST.Resources;

namespace NoteTagr.Api.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.UserName, resource.Password);
    }
}