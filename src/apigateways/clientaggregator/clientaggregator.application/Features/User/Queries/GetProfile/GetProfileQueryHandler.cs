﻿using clientaggregator.application.Contracts.Infrastructure.User;
using clientaggregator.application.Contracts.Models.Profile;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace clientaggregator.application.Features.User.Queries.GetProfile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, CustomerProfileDto>
    {
        private readonly ILogger<GetProfileQueryHandler> _logger;
        private readonly IUserService _customerService;

        public GetProfileQueryHandler(ILogger<GetProfileQueryHandler> logger, IUserService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        public async Task<CustomerProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _customerService.GetProfile(request.UserId);
            return profile;
        }
    }
}
