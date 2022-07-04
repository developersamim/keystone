using System.Linq.Expressions;
using AutoMapper;
using common.shared.User.Dto;
using MediatR;
using Microsoft.Extensions.Logging;
using user.application.Contracts.Persistence;
using user.domain;

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
        Expression<Func<VerifyEmail, bool>> predicate = uc => uc.UserId == request.UserId;
        Func<IQueryable<VerifyEmail>, IOrderedQueryable<VerifyEmail>> query = uc => uc.OrderByDescending(x => x.CreatedDate);
        var response = await verifyEmailRepository.GetAsync(predicate, query, "", true);
        var result = mapper.Map<VerifyEmailDto>(response.FirstOrDefault());

        return result;
    }
}

