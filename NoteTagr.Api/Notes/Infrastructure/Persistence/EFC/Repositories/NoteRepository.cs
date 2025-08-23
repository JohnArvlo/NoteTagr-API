using Microsoft.EntityFrameworkCore;
using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Shared.Persistence.EFC.Configuration;
using NoteTagr.Api.Shared.Persistence.EFC.Repositories;

namespace NoteTagr.Api.Notes.Infrastructure.Persistence.EFC.Repositories;

public class NoteRepository(AppDbContext context) : BaseRepository<Note>(context), 
    INoteRepository
{
    public async Task<Note?> GetNoteWithTagsAsync(int id)
    {
        return await Context.Notes
            .Include(n => n.Tags)
            .FirstOrDefaultAsync(n => n.Id == id);
    }
    
    public async Task<IEnumerable<Note>> ListWithTagsAsync()
    {
        return await Context.Notes
            .Include(n => n.Tags)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Note>> ListByUserIdWithTagsAsync(int userId)
    {
        return await Context.Notes
            .Include(n => n.Tags)
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }
}