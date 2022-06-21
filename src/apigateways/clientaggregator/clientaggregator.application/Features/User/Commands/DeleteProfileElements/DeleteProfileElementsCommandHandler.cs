using clientaggregator.application.Contracts.Infrastructure.User;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace clientaggregator.application.Features.User.Commands.DeleteProfileElements
{
    public class DeleteProfileElementsCommandHandler : IRequestHandler<DeleteProfileElementsCommand, Unit>
    {
        private readonly ILogger<DeleteProfileElementsCommandHandler> _logger;
        private readonly IUserService _profileService;

        public DeleteProfileElementsCommandHandler(ILogger<DeleteProfileElementsCommandHandler> logger, IUserService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        public async Task<Unit> Handle(DeleteProfileElementsCommand request, CancellationToken cancellationToken)
        {
            await _profileService.DeleteProfileElements(request.UserId, request.Keys);

            return Unit.Value;
        }
    }
}
