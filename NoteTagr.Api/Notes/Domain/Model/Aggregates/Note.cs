using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using NoteTagr.Api.Notes.Domain.Model.Commands;

namespace NoteTagr.Api.Notes.Domain.Model.Aggregates;

public class Note : IEntityWithCreatedUpdatedDate
{
    public int Id {get; set;}
    public string Title {get; set;}
    public string Content {get; set;}
    public bool Archived {get; set;}

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate {get; set;}
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate {get; set;}
    
    public Note(){}

    public Note(string title, string content)
    {
        Title = title;
        Content = content;
    }

    public Note(CreateNoteCommand command)
    {
        Title = command.Title;
        Content = command.Content;
    }

    public void UpdateInformation(string title, string content, bool archived)
    {
        Title = title;
        Content = content;
        Archived = archived;
    }
}
