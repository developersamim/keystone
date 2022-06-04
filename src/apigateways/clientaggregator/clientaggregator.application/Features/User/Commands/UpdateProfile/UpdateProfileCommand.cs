using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientaggregator.application.Features.User.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public Dictionary<string, object> Claims { get; set; }
    }
}
