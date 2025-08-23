using NoteTagr.Api.Notes.Domain.Model.Aggregates;

namespace NoteTagr.Api.IAM.Domain.Model.Aggregates;

public class User(string username, string passwordHash)
{
    public int Id { get; set; }
    public string Username { get; set; } = username;
    public string PasswordHash { get; set; } = passwordHash;

    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    
    public User UpdateUsername(string newUsername)
    {
        this.Username = newUsername;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        this.PasswordHash = passwordHash;
        return this;
    }
    
}