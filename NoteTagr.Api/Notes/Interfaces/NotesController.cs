using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using NoteTagr.Api.IAM.Domain.Model.Aggregates;
using NoteTagr.Api.Notes.Domain.Model.Commands;
using NoteTagr.Api.Notes.Domain.Model.Queries;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Notes.Interfaces.Rest.Resources;
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
    [Authorize] // Usa el de Microsoft
    public async Task<IActionResult> CreateNote(CreateNoteResource resource)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var command = new CreateNoteCommand(resource.Title, resource.Content, userId);
        var note = await noteCommandService.Handle(command);

        if (note == null)
            return BadRequest();

        var noteResource = NoteResourceFromEntityAssembler.ToResourceFromEntity(note);
        return Ok(noteResource);
    }



    [HttpDelete("{noteId:int}")]
    [Authorize]

    public async Task<IActionResult> DeleteNote([FromRoute] int noteId)
    {
        var deleteNoteCommand = new DeleteNoteByIdCommand(noteId);
        var isDeleted = await noteCommandService.Handle(deleteNoteCommand);
        if(!isDeleted) return NotFound();
        return NoContent();
    }

    [HttpGet("{noteId:int}")]
    [Authorize]

    public async Task<IActionResult> GetNoteById([FromRoute] int noteId)
    {
        var note = await noteQueryService.Handle(new GetNoteByIdQuery(noteId));
        if (note == null) return NotFound();
        var noteResource = NoteResourceFromEntityAssembler.ToResourceFromEntity(note);
        return Ok(noteResource);
    }
    
    [HttpGet]
    [Authorize]

    public async Task<IActionResult> GetAllNotes()
    {
        var getAllNotesQuery = new GetAllNotesQuery();
        var notes = await noteQueryService.Handle(getAllNotesQuery);
        var noteResources = notes.Select(NoteResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(noteResources);
    }
    
    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetAllNotesByUserId()
    {
        
        var userIdString = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString == null) return Unauthorized();

        var userId = int.Parse(userIdString);
        
        var getAllNotesByUserIdQuery = new GetAllNotesByUserIdQuery(userId);
        var notes = await noteQueryService.Handle(getAllNotesByUserIdQuery);
        var noteResources = notes.Select(NoteResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(noteResources);
    }

    [HttpPut("{noteId:int}")]
    [Authorize]

    public async Task<IActionResult> UpdateNote([FromRoute] int noteId, UpdateNoteResource resource)
    {
        var updateNoteCommand = new UpdateNoteCommand(noteId, resource.Title, resource.Content, resource.Archived);
        var note = await noteCommandService.Handle(updateNoteCommand);
        if(note == null) return NotFound();
        var noteResource = NoteResourceFromEntityAssembler.ToResourceFromEntity(note);
        return Ok(noteResource);
    }
    
    //add tags
    [HttpPatch("{noteId:int}/{tagId:int}")]
    [Authorize]

    public async Task<IActionResult> addTagsToNote([FromRoute] int noteId, [FromRoute] int tagId)
    {
        var note = await noteCommandService.Handle(noteId, tagId);
        if(note == null) return NotFound();
        var noteResource = NoteResourceFromEntityAssembler.ToResourceFromEntity(note);
        return Ok(noteResource);
    }
    
    //remove tags
    [HttpDelete("{noteId:int}/{tagId:int}")]
    [Authorize]

    public async Task<IActionResult> deleteTagsFromNote([FromRoute] int noteId, [FromRoute] int tagId)
    {
        var command = new DeleteTagFromNoteCommand(noteId, tagId);
        var note = await noteCommandService.Handle(command);
        if(note == null) return NotFound();
        var noteResource = NoteResourceFromEntityAssembler.ToResourceFromEntity(note);
        return Ok(noteResource);
    }
}