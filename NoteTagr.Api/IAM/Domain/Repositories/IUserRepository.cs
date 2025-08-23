using NoteTagr.Api.IAM.Domain.Model.Aggregates;
using NoteTagr.Api.Shared.Domain.Repositories;

namespace NoteTagr.Api.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    bool ExistsByUsername(string username);
}