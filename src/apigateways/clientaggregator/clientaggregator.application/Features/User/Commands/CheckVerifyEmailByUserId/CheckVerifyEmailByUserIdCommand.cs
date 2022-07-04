using System;
using MediatR;

namespace clientaggregator.application.Features.User.Commands.CheckVerifyEmailByUserId;

public class CheckVerifyEmailByUserIdCommand : IRequest<bool>
{
	public string UserId { get; set; }
	public string Code { get; set; }
}

