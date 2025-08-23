using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using NoteTagr.Api.Notes.Domain.Model.Commands;

namespace NoteTagr.Api.Notes.Domain.Model.Aggregates;

public class Tag : IEntityWithCreatedUpdatedDate
{
    
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate {get; set;}
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate {get; set;}

    public Tag(){}

    public Tag(string title, string? description)
    {
        Title = title;
        Description = description;
    }

    public Tag(CreateTagCommand command)
    {
        Title = command.Title;
        Description = command.Description;
    }

    public void UpdateInformation(string title, string? description)
    {
        Title = title;
        Description = description;
    }
}