﻿using clientaggregator.application.Contracts.Infrastructure.User;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace clientaggregator.application.Features.User.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Unit>
    {
        private readonly ILogger<UpdateProfileCommandHandler> _logger;
        private readonly IUserService _profileService;

        public UpdateProfileCommandHandler(ILogger<UpdateProfileCommandHandler> logger, IUserService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            await _profileService.UpdateProfile(request.UserId, request.Claims);

            return Unit.Value;
        }
    }
}
