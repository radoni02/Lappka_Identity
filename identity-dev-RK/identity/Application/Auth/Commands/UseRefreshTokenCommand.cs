using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands
{
    public record UseRefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand
    {
        public Guid TokenCatchId { get; } = Guid.NewGuid();
    }

}
