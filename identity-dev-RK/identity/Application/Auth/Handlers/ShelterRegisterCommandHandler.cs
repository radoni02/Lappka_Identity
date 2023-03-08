using Application.Auth.Commands;
using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Handlers
{
    public class ShelterRegisterCommandHandler : ICommandHandler<ShelterRegisterCommand>
    {
        public async Task HandleAsync(ShelterRegisterCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
