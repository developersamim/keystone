﻿using AutoMapper;
using clientaggregator.application.Contracts.Infrastructure.User;
using common.infrastructure;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace clientaggregator.infrastructure.User
{
    public partial class UserService : BaseService<IUserService>, IUserService
    {
        private const string ControllerUrl = "user";

        public UserService(ILogger<IUserService> logger, HttpClient client, IMapper mapper)
            : base(logger, client, mapper)
        {
        }
    }
}
