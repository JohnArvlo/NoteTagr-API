using NoteTagr.Api.IAM.Domain.Model.Aggregates;

namespace NoteTagr.Api.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}