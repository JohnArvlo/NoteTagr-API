using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NoteTagr.Api.Notes.Domain.Model.Commands;
using NoteTagr.Api.Notes.Domain.Model.Queries;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Notes.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace NoteTagr.Api.Notes.Interfaces.Rest;

[ApiController]
[Route("api/v1/tags")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Tags Endpoints")]
public class TagsController(ITagCommandService tagCommandService,
    ITagQueryService tagQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTag(CreateTagCommand command)
    {
        var tag = await tagCommandService.Handle(command);
        if(tag == null)
            return BadRequest();
        var tagResource = TagResourceFromEntityAssembler.ToResourceFromEntity(tag);
        return Ok(tagResource);
    }

    [HttpDelete("{tagId:int}")]
    public async Task<IActionResult> DeleteTag([FromRoute] int tagId)
    {
        var deleteTagCommand = new DeleteTagByIdCommand(tagId);
        var isDeleted = await tagCommandService.Handle(deleteTagCommand);
        if(!isDeleted) return NotFound();
        return NoContent();
    }

    [HttpGet("{tagId:int}")]
    public async Task<IActionResult> GetTagById([FromRoute] int tagId)
    {
        var tag = await tagQueryService.Handle(new GetTagByIdQuery(tagId));
        if (tag == null) return NotFound();
        var tagResource = TagResourceFromEntityAssembler.ToResourceFromEntity(tag);
        return Ok(tagResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTags()
    {
        var getAllTagsQuery = new GetAllTagsQuery();
        var tags = await tagQueryService.Handle(getAllTagsQuery);
        var tagResources = tags.Select(TagResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tagResources);
    }
}