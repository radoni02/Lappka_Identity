using Application.Auth.Commands;
using Application.Exceptions.TokenException;
using Application.Services;
using Convey.CQRS.Commands;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Handlers
{
    public class RevokeTokenCommandHandler : ICommandHandler<RevokeTokenCommand>
    {
        private readonly IIdentityRepository _repo;
        private readonly IJwtHandler _jwtHandler;        
        private readonly ITokenRepository _tokenRepo;

        public RevokeTokenCommandHandler(IIdentityRepository repo, ITokenRepository tokenRepo, IJwtHandler jwtHandler )
        {
            _repo = repo;
            _tokenRepo = tokenRepo;
            _jwtHandler = jwtHandler;

        }

        public async Task HandleAsync(RevokeTokenCommand command, CancellationToken cancellationToken = default)
        {
            var id = Guid.Empty;
            id = _jwtHandler.ReadAccessToken(command.AccessToken);
            var AppRefreshToken = await _tokenRepo.GetRefreshToken(command.RefreshToken, id);
            if (AppRefreshToken == null)
            {
                 throw new InvalidRefreshTokenException();
            }
            await _tokenRepo.RemoveRefreshTokenAsync(AppRefreshToken);
        }
    }
}
