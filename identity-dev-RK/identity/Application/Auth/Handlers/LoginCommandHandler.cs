using Application.Auth.Commands;
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
    public class LoginCommandHandler : ICommandHandler<LoginCommand>
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly ITokenRepository _tokenRepository;
        private readonly IIdentityRepository _repo;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserRequestStorage _userRequestStorage;

        public LoginCommandHandler(IIdentityRepository repo, SignInManager<AppUser> signInManager,
            ITokenRepository tokenRepository,IJwtHandler jwtHandler, IUserRequestStorage userRequestStorage)
        {
            _repo = repo;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
            _jwtHandler = jwtHandler;
            _userRequestStorage = userRequestStorage;
         
        }

        public async Task HandleAsync(LoginCommand command, CancellationToken cancellationToken= new CancellationToken())
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(command.EmailAddress);
            if (user == null)
            {
                throw new UserNotFoundByEmailException(command.EmailAddress);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
            if (!result.Succeeded)
            {
                throw new CheckPasswordSignInException();
            }
            else if (user.EmailConfirmed == false)
            {
                throw new AccountNotConfirmedException();
            }
            else if(user.Role == null)
            {
                throw new UserNotAssignToAnyRoleException();
            }
            var refreshToken = _jwtHandler.CreateRefreshToken();
            var accessToken = _jwtHandler.CreateAccessToken(user.Id, user.Role.ToString());
            var appToken = new AppToken()
            {
                LoginProvider = "Lappka",
                Name = Guid.NewGuid().ToString(),
                Value = refreshToken,
                UserId = user.Id
            };
            await _tokenRepository.AddRefreshTokenAsync(appToken);
            _userRequestStorage.SetToken(command.AccessTokenCacheId, accessToken);
            _userRequestStorage.SetToken(command.RefreshTokenCacheId, refreshToken);

        }
    }
}
