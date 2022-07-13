using System;
using common.emailsender;
using common.entityframework;
using MediatR;
using Microsoft.Extensions.Logging;
using user.application.Contracts.Persistence;
using user.domain;

namespace user.application.Features.Users.Commands.SendEmailVerifyCode;

public class SendEmailVerifyCodeCommandHandler : IRequestHandler<SendEmailVerifyCodeCommand>
{
    private readonly ILogger<SendEmailVerifyCodeCommandHandler> logger;
    private readonly IEmailSender emailSender;
    private readonly IVerifyEmailRepository verifyEmailRepository;
    private readonly IUserRepository userRepository;
    private readonly IUserClaimRepository userClaimRepository;
    private readonly IUnitOfWork unitOfWork;

    public SendEmailVerifyCodeCommandHandler(ILogger<SendEmailVerifyCodeCommandHandler> logger, IEmailSender emailSender, IVerifyEmailRepository verifyEmailRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, IUserClaimRepository userClaimRepository)
    {
        this.logger = logger;
        this.emailSender = emailSender;
        this.verifyEmailRepository = verifyEmailRepository;
        this.userRepository = userRepository;
        this.userClaimRepository = userClaimRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(SendEmailVerifyCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new Exception($"User {request.UserId} does not exist. Error at SendVerifyEmailCodeHandler");

        var userclaims = await userClaimRepository.GetClaimsAsync(user);

        // create VerifyEmail row
        var verifyEmailEntity = new VerifyEmail(request.UserId);
        verifyEmailRepository.Add(verifyEmailEntity);
        await unitOfWork.SaveChangesAsync();


        // send email to user with unique code
        // so that user can verify their email
        var usersToBeEmailed = new Dictionary<string, string>
                {
                    { userclaims.FirstOrDefault(x => x.Type == "given_name").Value, user.Email }
                };
        var message = new EmailMessage(usersToBeEmailed, verifyEmailEntity.Code);
        emailSender.SendCodeToVerifyEmail(message);

        return Unit.Value;
    }
}

