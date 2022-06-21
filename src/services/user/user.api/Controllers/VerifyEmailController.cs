using common.api.authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user.application.Features.Users.Queries.GetVerifyEmailByUserId;

namespace user.api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthConstant.KnownAuthorizationPolicyName.ServerAccess)]
public class VerifyEmailController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IMediator mediator;

    public VerifyEmailController(ILogger<UserController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetVerifyEmail([FromRoute] string userId)
    {
        var query = new GetVerifyEmailByUserIdQuery(userId);
        var result = await mediator.Send(query);

        return Ok(result);  
    }
}
