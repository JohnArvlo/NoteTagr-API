using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using NoteTagr.Api.Notes.Domain.Model.Aggregates;

namespace NoteTagr.Api.Shared.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //bounded context Notes
        builder.Entity<Note>().HasKey(n => n.Id);
        builder.Entity<Note>().Property(n => n.Id).ValueGeneratedOnAdd();
        builder.Entity<Note>().Property(n => n.Title).IsRequired().HasMaxLength(60);
        builder.Entity<Note>().Property(n => n.Content).IsRequired();
        builder.Entity<Note>().Property(n => n.Archived).IsRequired();
        
    }
}