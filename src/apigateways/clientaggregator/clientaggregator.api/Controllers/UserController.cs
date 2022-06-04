using clientaggregator.api.Models;
using clientaggregator.application.Contracts.Models.Profile;
using clientaggregator.application.Features.User.Commands.UpdateProfile;
using clientaggregator.application.Features.User.Queries.GetProfile;
using common.api.authentication;
using common.utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace clientaggregator.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthConstant.KnownAuthorizationPolicyName.ClientAccess)]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets the profile information of the current logged in user
        /// </summary>
        /// <returns>Profile Information</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerProfileDto>> GetProfile()
        {
            var query = new GetProfileQuery()
            {
                UserId = User.UserId()
            };

            var result = await mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Update profile
        /// </summary>
        /// <param name="request"></param>
        /// <returns>No content</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateProfile([FromBody] ProfileUpdateModel request)
        {
            var keyValuePair = new Dictionary<string, object>
            {
                [Constant.KnownUserClaim.GivenName] = request.GivenName,
                [Constant.KnownUserClaim.FamilyName] = request.FamilyName,
                [Constant.KnownUserClaim.Birthdate] = request.Birthdate.Date.ToString("yyyy-MM-dd"),
                [Constant.KnownUserClaim.ProfileVerified] = true
            };



            var query = new UpdateProfileCommand()
            {
                UserId = User.UserId(),
                Claims = keyValuePair
            };

            var result = await mediator.Send(query);

            return NoContent();
        }
    }
}
