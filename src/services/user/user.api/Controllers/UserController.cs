﻿using common.api.authentication;
using common.utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user.application.Features.Users.Commands.CreateUser;
using user.application.Features.Users.Commands.DeleteProfileElement;
using user.application.Features.Users.Commands.UpdateProfileElement;
using user.application.Features.Users.Queries.GetUserProfileByUserId;
using user.application.Features.Users.Queries.GetUsers;
using user.application.Models;

namespace user.api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthConstant.KnownAuthorizationPolicyName.ServerAccess)]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IMediator mediator;

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    /// <summary>
    /// Get User
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> GetUser([FromQuery] string? userId)
    {
        if (userId == null)
            userId = User.UserId();
        var query = new GetUserProfileByUserIdQuery(userId);
        var result = await mediator.Send(query);

        return Ok(result);  
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<ActionResult> GetUsers()
    {
        var query = new GetUsersQuery();
        var result = await mediator.Send(query);

        return Ok(result);
    }

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Post([FromBody] CreateUserCommand command)
    {
        await mediator.Send(command);

        return StatusCode(204);
    }

    /// <summary>
    /// Update profile element
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="profileElements"></param>
    /// <returns></returns>
    [HttpPut("{userId}")]
    public async Task<ActionResult> UpdateProfileElement([FromRoute] string userId, [FromBody] Dictionary<string, object> profileElements)
    {
        if (userId is null)
            userId = User.UserId();

        var command = new UpdateProfileElementCommand()
        {
            UserId = userId,
            KeyValuePairs = profileElements
        };

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteProfileElement([FromRoute] string? userId, [FromQuery] List<string> keys)
    {
        if (userId is null)
            userId = User.UserId();

        var command = new DeleteProfileElementCommand()
        {
            UserId = userId,
            Keys = keys
        };

        await mediator.Send(command);

        return NoContent();
    }
}
