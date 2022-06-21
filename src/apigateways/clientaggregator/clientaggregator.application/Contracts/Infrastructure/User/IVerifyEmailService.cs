using System;
using System.Threading.Tasks;
using common.shared.User.Dto;

namespace clientaggregator.application.Contracts.Infrastructure.User;

public interface IVerifyEmailService
{
    Task<VerifyEmailDto> GetVerifyEmail(Guid userId);
}

