using common.entityframework;
using user.domain;

namespace user.application.Contracts.Persistence;

public interface IVerifyEmailRepository : IAsyncRepository<VerifyEmail>
{
}
