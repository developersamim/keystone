using System;
using System.Threading.Tasks;
using clientaggregator.application.Contracts.Infrastructure.User;
using clientaggregator.application.Features.User.Commands.CheckVerifyEmailByUserId;
using common.api.authentication;
using common.shared;
using common.utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace clientaggregator.api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthConstant.KnownAuthorizationPolicyName.ClientAccess)]
public class VerifyEmailController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IMediator mediator;
    private readonly IVerifyEmailService verifyEmailService;

    public VerifyEmailController(ILogger<UserController> logger, IMediator mediator, IVerifyEmailService verifyEmailService)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.verifyEmailService = verifyEmailService;
    }

    [HttpPost]
    public async Task<ActionResult> CheckVerifyEmailCode([FromBody] CheckVerifyEmailCodeRequestDto request)
    {
        var command = new CheckVerifyEmailByUserIdCommand
        {
            UserId = User.UserId(),
            Code = request.Code
        };
        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> SendVerifyEmailCode()
    {
        await verifyEmailService.SendVerifyEmailCode(User.UserId());

        return NoContent();
    }
}
