namespace NoteTagr.Api.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}