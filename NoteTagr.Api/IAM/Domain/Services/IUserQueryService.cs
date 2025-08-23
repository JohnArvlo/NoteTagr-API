using NoteTagr.Api.IAM.Domain.Model.Aggregates;
using NoteTagr.Api.IAM.Domain.Model.Queries;

namespace NoteTagr.Api.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByUsernameQuery query);
}