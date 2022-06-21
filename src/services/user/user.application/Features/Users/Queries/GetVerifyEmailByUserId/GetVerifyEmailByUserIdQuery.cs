using common.shared.User.Dto;
using MediatR;

namespace user.application.Features.Users.Queries.GetVerifyEmailByUserId;

public class GetVerifyEmailByUserIdQuery : IRequest<VerifyEmailDto>
{
	public string UserId { get; set; }

	public GetVerifyEmailByUserIdQuery(string userId)
	{
		UserId = userId;
	}
}

