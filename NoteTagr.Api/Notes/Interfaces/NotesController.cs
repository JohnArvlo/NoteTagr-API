using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using NoteTagr.Api.Notes.Domain.Model.Commands;
using NoteTagr.Api.Notes.Domain.Model.Queries;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Notes.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace NoteTagr.Api.Notes.Interfaces;

[ApiController]
[Route("api/v1/notes")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Notes Endpoints")]
public class NotesController(INoteCommandService noteCommandService,
    INoteQueryService noteQueryService): ControllerBase
{
    
    
    [HttpPost]
    public async Task<IActionResult> CreateNote(CreateNoteCommand command)
    {
        var note = await noteCommandService.Handle(command);
        if(note == null)
            return BadRequest();
        var noteResource = NoteResourceFromEntityAssembler.ToResourceFromEntity(note);
        return Ok(noteResource);
    }

    [HttpDelete("{noteId:int}")]
    public async Task<IActionResult> DeleteNote([FromRoute] int noteId)
    {
        var deleteNoteCommand = new DeleteNoteByIdCommand(noteId);
        var isDeleted = await noteCommandService.Handle(deleteNoteCommand);
        if(!isDeleted) return NotFound();
        return NoContent();
    }

    [HttpGet("{noteId:int}")]
    public async Task<IActionResult> GetNoteById([FromRoute] int noteId)
    {
        var note = await noteQueryService.Handle(new GetNoteByIdQuery(noteId));
        if (note == null) return NotFound();
        var noteResource = NoteResourceFromEntityAssembler.ToResourceFromEntity(note);
        return Ok(noteResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        var getAllNotesQuery = new GetAllNotesQuery();
        var notes = await noteQueryService.Handle(getAllNotesQuery);
        var noteResources = notes.Select(NoteResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(noteResources);
    }
    
}