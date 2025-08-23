using NoteTagr.Api.IAM.Domain.Model.Aggregates;
using NoteTagr.Api.IAM.Domain.Model.Queries;
using NoteTagr.Api.IAM.Domain.Repositories;
using NoteTagr.Api.IAM.Domain.Services;

namespace NoteTagr.Api.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.UserId);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
}