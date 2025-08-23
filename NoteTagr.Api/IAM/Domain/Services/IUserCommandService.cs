using NoteTagr.Api.IAM.Domain.Model.Aggregates;
using NoteTagr.Api.IAM.Domain.Model.Commands;

namespace NoteTagr.Api.IAM.Domain.Services;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
    Task<(User user, string token)> Handle(SignInCommand command);
}