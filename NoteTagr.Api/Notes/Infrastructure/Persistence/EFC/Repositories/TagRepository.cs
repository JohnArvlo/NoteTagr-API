using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Shared.Persistence.EFC.Configuration;
using NoteTagr.Api.Shared.Persistence.EFC.Repositories;

namespace NoteTagr.Api.Notes.Infrastructure.Persistence.EFC.Repositories;

public class TagRepository(AppDbContext context) : BaseRepository<Tag>(context),
    ITagRepository
{
    
}