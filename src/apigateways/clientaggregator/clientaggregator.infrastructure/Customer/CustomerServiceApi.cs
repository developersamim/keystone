using AutoMapper;
using clientaggregator.application.Contracts.Infrastructure.Customer;
using common.infrastructure;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace clientaggregator.infrastructure.Customer
{
    public partial class CustomerServiceApi : BaseService<ICustomerService>, ICustomerService
    {
        private const string ControllerUrl = "user";

        public CustomerServiceApi(ILogger<ICustomerService> logger, HttpClient client, IMapper mapper)
            : base(logger, client, mapper)
        {
        }
    }
}
