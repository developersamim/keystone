using AutoMapper;
using common.shared.User.Dto;
using MediatR;
using Microsoft.Extensions.Logging;
using user.application.Contracts.Persistence;

namespace user.application.Features.Users.Queries.GetVerifyEmailByUserId;

public class GetVerifyEmailByUserIdQueryHandler : IRequestHandler<GetVerifyEmailByUserIdQuery, VerifyEmailDto>
{
    private readonly ILogger<GetVerifyEmailByUserIdQueryHandler> logger;
    private readonly IVerifyEmailRepository verifyEmailRepository;
    private readonly IMapper mapper;

	public GetVerifyEmailByUserIdQueryHandler(ILogger<GetVerifyEmailByUserIdQueryHandler> logger, IVerifyEmailRepository verifyEmailRepository, IMapper mapper)
	{
        this.logger = logger;
        this.verifyEmailRepository = verifyEmailRepository;
        this.mapper = mapper;
	}

    public async Task<VerifyEmailDto> Handle(GetVerifyEmailByUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = await verifyEmailRepository.FirstOrDefaultAsync(x => x.UserId == request.UserId);
        var result = mapper.Map<VerifyEmailDto>(response);

        return result;
    }
}

