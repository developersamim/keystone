using clientaggregator.application.Contracts.Models.Profile;
using MediatR;

namespace clientaggregator.application.Features.Profile.Queries.GetProfile
{
    public class GetProfileQuery : IRequest<CustomerProfileDto>
    {
        public string UserId { get; set; }
    }
}
