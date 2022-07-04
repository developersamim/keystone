using System;
using MediatR;

namespace user.application.Features.Users.Commands.SendEmailVerifyCode;

public class SendEmailVerifyCodeCommand : IRequest
{
    public string UserId { get; set; }
}

