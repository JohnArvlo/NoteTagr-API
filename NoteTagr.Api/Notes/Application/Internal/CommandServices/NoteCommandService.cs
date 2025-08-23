using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Commands;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Shared.Domain.Repositories;

namespace NoteTagr.Api.Notes.Application.Internal.CommandServices;

public class NoteCommandService(INoteRepository noteRepository, IUnitOfWork unitOfWork) : INoteCommandService
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
}