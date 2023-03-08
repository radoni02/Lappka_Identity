using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands
{
    public record LoginCommand(string EmailAddress, string Password) : ICommand
    {
        public Guid AccessTokenCacheId { get; } = Guid.NewGuid();
        public Guid RefreshTokenCacheId { get; } = Guid.NewGuid();
    }
}
