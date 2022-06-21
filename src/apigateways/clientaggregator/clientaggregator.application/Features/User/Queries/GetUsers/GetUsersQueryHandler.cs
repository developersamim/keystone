using clientaggregator.application.Contracts.Infrastructure.User;
using clientaggregator.application.Contracts.Models.Profile;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace clientaggregator.application.Features.User.Queries.GetUsers
{
    public class GetProfileQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<CustomerProfileDto>>
    {
        private readonly ILogger<GetProfileQueryHandler> _logger;
        private readonly IUserService _customerService;

        public GetProfileQueryHandler(ILogger<GetProfileQueryHandler> logger, IUserService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        public async Task<IEnumerable<CustomerProfileDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _customerService.GetUsers();
            return users;
        }
    }
}
