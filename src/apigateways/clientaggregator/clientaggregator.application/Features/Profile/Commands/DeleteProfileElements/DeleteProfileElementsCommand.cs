using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientaggregator.application.Features.Profile.Commands.DeleteProfileElements
{
    public class DeleteProfileElementsCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public List<string> Keys { get; set; }
    }
}
