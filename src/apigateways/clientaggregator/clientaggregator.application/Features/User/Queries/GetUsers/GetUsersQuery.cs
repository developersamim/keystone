using clientaggregator.application.Contracts.Models.Profile;
using MediatR;
using System.Collections.Generic;

namespace clientaggregator.application.Features.User.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<CustomerProfileDto>>
    {
        public string UserId { get; set; }
    }
}
