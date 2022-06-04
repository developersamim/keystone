using clientaggregator.api.Models;
using clientaggregator.application.Contracts.Models.Profile;
using clientaggregator.application.Features.Profile.Commands.UpdateProfile;
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
    //[Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IMediator mediator;

        public ProfileController(ILogger<ProfileController> logger, IMediator mediator)
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
            //var profile = await _profileService.GetProfile(User.UserId());
            //return Ok(profile);

            //var query = new GetProfileQuery()
            //{
            //    UserId = User.UserId()
            //};

            //await mediator.Send(query);

            return Ok("all good");
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
