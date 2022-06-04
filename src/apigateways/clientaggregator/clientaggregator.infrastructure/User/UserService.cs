using AutoMapper;
using clientaggregator.application.Contracts.Infrastructure.Customer;
using common.infrastructure;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace clientaggregator.infrastructure.User
{
    public partial class UserService : BaseService<ICustomerService>, ICustomerService
    {
        private const string ControllerUrl = "user";

        public UserService(ILogger<ICustomerService> logger, HttpClient client, IMapper mapper)
            : base(logger, client, mapper)
        {
        }
    }
}
