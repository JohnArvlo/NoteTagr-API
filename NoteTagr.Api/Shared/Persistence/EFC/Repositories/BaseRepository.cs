using Microsoft.EntityFrameworkCore;
using NoteTagr.Api.Shared.Domain.Repositories;
using NoteTagr.Api.Shared.Persistence.EFC.Configuration;

namespace NoteTagr.Api.Shared.Persistence.EFC.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;

    protected BaseRepository(AppDbContext context)
    {
        Context = context;
    }
    
    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public async Task<TEntity?> FindByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }
    
    public async Task<IEnumerable<TEntity>> ListByIdsAsync(IEnumerable<int> ids)
    {
        return await Context.Set<TEntity>()
            .Where(e => ids.Contains(EF.Property<int>(e, "Id")))
            .ToListAsync();
    }
}