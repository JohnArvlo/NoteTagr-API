using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteTagr.Api.Notes.Domain.Model.Commands;
using NoteTagr.Api.Notes.Domain.Model.Queries;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Notes.Interfaces.Rest.Resources;
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
    [Authorize]
    public async Task<IActionResult> CreateTag(CreateTagResource resource)
    {
        
        var userIdString = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString == null) return Unauthorized();

        var userId = int.Parse(userIdString);
        
        var command = new CreateTagCommand(resource.Title, resource.Description, userId);
        
        var tag = await tagCommandService.Handle(command);
        if(tag == null)
            return BadRequest();
        var tagResource = TagResourceFromEntityAssembler.ToResourceFromEntity(tag);
        return Ok(tagResource);
    }

    [HttpDelete("{tagId:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteTag([FromRoute] int tagId)
    {
        var deleteTagCommand = new DeleteTagByIdCommand(tagId);
        var isDeleted = await tagCommandService.Handle(deleteTagCommand);
        if(!isDeleted) return NotFound();
        return NoContent();
    }

    [HttpGet("{tagId:int}")]
    [Authorize]
    public async Task<IActionResult> GetTagById([FromRoute] int tagId)
    {
        var tag = await tagQueryService.Handle(new GetTagByIdQuery(tagId));
        if (tag == null) return NotFound();
        var tagResource = TagResourceFromEntityAssembler.ToResourceFromEntity(tag);
        return Ok(tagResource);
    }
    
    [HttpGet]
    [Authorize]

    public async Task<IActionResult> GetAllTags()
    {
        var getAllTagsQuery = new GetAllTagsQuery();
        var tags = await tagQueryService.Handle(getAllTagsQuery);
        var tagResources = tags.Select(TagResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tagResources);
    }
    
    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetAllTagsByUserId()
    {
        
        var userIdString = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString == null) return Unauthorized();

        var userId = int.Parse(userIdString);
        
        var getAllTagsByUserIdQuery = new GetAllTagsByUserIdQuery(userId);
        var tags = await tagQueryService.Handle(getAllTagsByUserIdQuery);
        var tagResources = tags.Select(TagResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tagResources);
    }

    [HttpPut("{tagId:int}")]
    [Authorize]

    public async Task<IActionResult> UpdateTag([FromRoute] int tagId, UpdateTagResource resource)
    {
        var updateTagCommand = new UpdateTagCommand(tagId, resource.Title, resource.Description);
        var tag = await tagCommandService.Handle(updateTagCommand);
        if (tag == null) return NotFound();
        var tagResource = TagResourceFromEntityAssembler.ToResourceFromEntity(tag);
        return Ok(tagResource);
    }
    
}