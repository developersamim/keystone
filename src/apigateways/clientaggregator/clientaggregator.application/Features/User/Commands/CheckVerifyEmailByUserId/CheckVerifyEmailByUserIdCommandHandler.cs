using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using clientaggregator.application.Contracts.Infrastructure.User;
using MediatR;
using Microsoft.Extensions.Logging;
using static common.utilities.Constant;

namespace clientaggregator.application.Features.User.Commands.CheckVerifyEmailByUserId;

public class CheckVerifyEmailByUserIdCommandHandler : IRequestHandler<CheckVerifyEmailByUserIdCommand, bool>
{
    private readonly ILogger<CheckVerifyEmailByUserIdCommandHandler> logger;
    private readonly IVerifyEmailService verifyEmailService;
    private readonly IUserService userService;

	public CheckVerifyEmailByUserIdCommandHandler(ILogger<CheckVerifyEmailByUserIdCommandHandler> logger, IVerifyEmailService verifyEmailService, IUserService userService)
	{
        this.logger = logger;
        this.verifyEmailService = verifyEmailService;
        this.userService = userService;
	}

    public async Task<bool> Handle(CheckVerifyEmailByUserIdCommand request, CancellationToken cancellationToken)
    {
        var result = await verifyEmailService.GetVerifyEmail(request.UserId);

        if (!result.IsCodeValid)
        {
            throw new Exception("Code has been expired");
        }

        if (result.Code != request.Code)
        {
            throw new Exception("Code does not match");
        }

        var claims = new Dictionary<string, object>
        {
            { KnownUserClaim.EmailVerified, true }
        };

        await userService.UpdateProfile(request.UserId, claims);

        return true;
    }
}

