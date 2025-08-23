using NoteTagr.Api.Notes.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Commands;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Shared.Domain.Repositories;

namespace NoteTagr.Api.Notes.Application.Internal.CommandServices;

public class TagCommandService(ITagRepository tagRepository, IUnitOfWork unitOfWork) : ITagCommandService
{
    public async Task<Tag?> Handle(CreateTagCommand command)
    {
        var tag = new Tag(command);
        await tagRepository.AddAsync(tag);
        await unitOfWork.CompleteAsync();
        return tag;
    }

    public async Task<bool> Handle(DeleteTagByIdCommand command)
    {
        var tag = await tagRepository.FindByIdAsync(command.TagId);
        if (tag == null)
            return false;
        tagRepository.Remove(tag);
        await unitOfWork.CompleteAsync();
        return true;
    }
}