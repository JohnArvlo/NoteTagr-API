namespace NoteTagr.Api.Notes.Domain.Model.Commands;

public record AddTagsToNoteCommand(int NoteId, List<int> TagIds);