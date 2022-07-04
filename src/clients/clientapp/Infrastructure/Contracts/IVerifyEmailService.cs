using System;
namespace clientapp.Infrastructure.Contracts;
using common.shared;
using common.shared.User.Dto;

public interface IVerifyEmailService
{
    Task<HttpResponseMessage> CheckVerifyEmailCode(CheckVerifyEmailCodeRequestDto request);
    Task<HttpResponseMessage> SendVerifyEmailCode();
}

