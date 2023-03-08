using Application.Auth.Commands;
using Application.Exceptions.TokenException;
using Application.Exceptions.UserException;
using Application.Services;
using Convey.CQRS.Commands;
using Core.Domain.Models;
using Core.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Handlers
{
    public class UseRefreshTokenCommandHandler : ICommandHandler<UseRefreshTokenCommand>
    {
        
        private readonly IIdentityRepository _repo;
        private readonly ITokenRepository _tokenRepo;
        private readonly IUserRequestStorage _userRequestStorage;
        private readonly IJwtHandler _jwtHandler; 

        public UseRefreshTokenCommandHandler( IIdentityRepository repo,IJwtHandler jwtHandler,
            ITokenRepository tokenRepo, IUserRequestStorage userRequestStorage)
        {
            
            _repo = repo;
            _tokenRepo = tokenRepo;
            _userRequestStorage = userRequestStorage;
            _jwtHandler = jwtHandler;
        }

        public async Task HandleAsync(UseRefreshTokenCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var id = Guid.Empty;
            id = _jwtHandler.ReadAccessToken(command.AccessToken);
            //tu dac walidacje try catch
            var user = await _repo.FindByIdAsync(id);
            if (user is null)
            {
                throw new UserNotFoundByIdException(id);
            }
            var appRefreshToken = await _tokenRepo.GetRefreshToken(command.RefreshToken, id);
            if (appRefreshToken is null)
            {
                throw new InvalidRefreshTokenException();
            }

            if (appRefreshToken.CreatedAt.AddDays(_jwtHandler.GetExpiredDays()) < DateTime.UtcNow)
            {
                await _tokenRepo.RemoveRefreshTokenAsync(appRefreshToken);
                throw new InvalidRefreshTokenException();
            }
            var accessToken = _jwtHandler.CreateAccessToken(id,user.Role.ToString()); 
            _userRequestStorage.SetToken(command.TokenCatchId, command.AccessToken);
        }
    }
}
