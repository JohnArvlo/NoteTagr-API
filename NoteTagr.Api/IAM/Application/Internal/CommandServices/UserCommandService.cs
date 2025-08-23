using NoteTagr.Api.IAM.Application.Internal.OutboundServices;
using NoteTagr.Api.IAM.Domain.Model.Aggregates;
using NoteTagr.Api.IAM.Domain.Model.Commands;
using NoteTagr.Api.IAM.Domain.Repositories;
using NoteTagr.Api.IAM.Domain.Services;
using NoteTagr.Api.Shared.Domain.Repositories;

namespace NoteTagr.Api.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IHashingService hashingService
) : IUserCommandService
{
    // inheritDoc
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} already exists");
        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating the user: {e.Message}");
        }
    }

    // inheritDoc
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
        if (user is null) throw new Exception($"User {command.Username} not found");
        if (!hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid password");
        var token = tokenService.GenerateToken(user);
        return (user, token);
    }
}