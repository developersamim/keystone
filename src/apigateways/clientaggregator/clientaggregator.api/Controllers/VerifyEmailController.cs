using System;
using System.Threading.Tasks;
using clientaggregator.application.Contracts.Infrastructure.User;
using common.api.authentication;
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
    private readonly IVerifyEmailService verifyEmailService;

    public VerifyEmailController(ILogger<UserController> logger, IVerifyEmailService verifyEmailService)
    {
        this.logger = logger;
        this.verifyEmailService = verifyEmailService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetVerifyEmail([FromRoute] string userId)
    {
        var result = await verifyEmailService.GetVerifyEmail(Guid.Parse(userId));
        return Ok(result);  
    }
}
