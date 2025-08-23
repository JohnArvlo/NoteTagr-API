using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteTagr.Api.IAM.Domain.Services;
using NoteTagr.Api.IAM.Interfaces.REST.Resources;
using NoteTagr.Api.IAM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace NoteTagr.Api.IAM.Interfaces.REST;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    // [Infrastructure.Pipeline.Middleware.Attributes.AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in to the platform",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "Authenticated user", typeof(AuthenticatedUserResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler
            .ToResourceFromEntity(authenticatedUser.user, authenticatedUser.token);
        return Ok(authenticatedUserResource);
    }
    
    // [Infrastructure.Pipeline.Middleware.Attributes.AllowAnonymous]
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Sign up",
        Description = "Sign up to the platform",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "User created successfully")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(signUpCommand);
        return Ok(new { message = "User created successfully" });
    }
}