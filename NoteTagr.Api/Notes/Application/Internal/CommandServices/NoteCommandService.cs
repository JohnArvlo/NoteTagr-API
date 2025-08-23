using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Commands;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Shared.Domain.Repositories;

namespace NoteTagr.Api.Notes.Application.Internal.CommandServices;

public class NoteCommandService(INoteRepository noteRepository, 
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : INoteCommandService
{
    public async Task<Note?> Handle(CreateNoteCommand command)
    {
        var note = new Note(command);
        await noteRepository.AddAsync(note);
        await unitOfWork.CompleteAsync();
        return note;
    }

    public async Task<bool> Handle(DeleteNoteByIdCommand command)
    {
        var note = await noteRepository.FindByIdAsync(command.NoteId);
        if(note == null)
            return false;
        noteRepository.Remove(note);
        await unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<Note?> Handle(UpdateNoteCommand command)
    {
        var note = await noteRepository.GetNoteWithTagsAsync(command.NoteId);
        if (note == null)
            return null;
        note.UpdateInformation(command.Title, command.Content, command.Archived);
        await unitOfWork.CompleteAsync();
        return note;
    }
    
    
    //add tags
    public async Task<Note?> Handle(int noteId, int tagId)
    {
        var note = await noteRepository.GetNoteWithTagsAsync(noteId);
        if (note == null) return null;
        var tag = await tagRepository.FindByIdAsync(tagId);
        if (tag == null) return null;
        
        note.Tags.Add(tag);
        await unitOfWork.CompleteAsync();
        return note;
    }
    
    //delete tag
    public async Task<Note?> Handle(DeleteTagFromNoteCommand command)
    {
        var note = await noteRepository.GetNoteWithTagsAsync(command.NoteId);
        if (note == null) return null;
        var tag = await tagRepository.FindByIdAsync(command.TagId);
        if (tag == null) return null;
        
        note.Tags.Remove(tag);
        await unitOfWork.CompleteAsync();
        return note;
    }
    
}