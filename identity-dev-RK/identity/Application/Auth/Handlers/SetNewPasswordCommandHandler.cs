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
    public class SetNewPasswordCommandHandler : ICommandHandler<SetNewPasswordCommand>
    {
        private readonly IIdentityRepository _repo;
        private readonly ITokenRepository _tokenRepo;
        private readonly UserManager<AppUser> _userManager;
        //private readonly IJwtClaimsReader _jwtClaimReader;

        public SetNewPasswordCommandHandler(IIdentityRepository repo, ITokenRepository tokenRepo, UserManager<AppUser> userManager/*, IJwtClaimsReader jwtClaimReader*/)
        {
            _repo = repo;
            _tokenRepo = tokenRepo;
            _userManager = userManager;
            //_jwtClaimReader = jwtClaimReader;
        }

        //private readonly IJwtClaimsReader _jwtClaimReader;
        public async Task HandleAsync(SetNewPasswordCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            if(command.Password!=command.NewPassword)
            {
                throw new PasswordMustBeSameException();
            }
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
            {
                throw new UserNotFoundByEmailException(command.Email);
            }
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, command.Token, command.Password);
            if(!resetPasswordResult.Succeeded)
            {
                throw new UnableToResetPasswordException();
            }
        }

    }
}
