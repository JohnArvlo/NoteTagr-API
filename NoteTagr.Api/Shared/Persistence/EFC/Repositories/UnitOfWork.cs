using NoteTagr.Api.Shared.Domain.Repositories;
using NoteTagr.Api.Shared.Persistence.EFC.Configuration;

namespace NoteTagr.Api.Shared.Persistence.EFC.Repositories;


public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}