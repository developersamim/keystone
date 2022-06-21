using AutoMapper;
using clientaggregator.application.Contracts.Infrastructure.User;
using common.infrastructure;
using common.shared.User.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace clientaggregator.infrastructure.User
{
    public partial class VerifyEmailService : BaseService<IVerifyEmailService>, IVerifyEmailService
    {
        private const string ControllerUrl = "verifyemail";

        public VerifyEmailService(ILogger<IVerifyEmailService> logger, HttpClient client, IMapper mapper)
            : base(logger, client, mapper)
        {
        }

        public async Task<VerifyEmailDto> GetVerifyEmail(Guid userId)
        {
            var response = await Client.GetFromJsonAsync<VerifyEmailDto>($"{ControllerUrl}/{userId}");

            return response;
        }
    }
}
