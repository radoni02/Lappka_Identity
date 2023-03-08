using Application.Auth.Commands;
using Application.Exceptions.TokenException;
using Application.Exceptions.UserException;
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
    public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityRepository _repo;
        public ConfirmEmailCommandHandler(UserManager<AppUser> userManager,IIdentityRepository repo)
        {
            _userManager = userManager;
            _repo = repo;
        }

        public async Task HandleAsync(ConfirmEmailCommand command, CancellationToken cancellationToken= new CancellationToken())
        {
            var user = await _repo.FindByEmailAsync(command.emailAddress);
            if(user is null)
            {
                throw new UserNotFoundByEmailException(command.emailAddress);
            }
            var result = await _userManager.ConfirmEmailAsync(user, command.token);
            if (!result.Succeeded)
            {
                throw new InvalidConfirmEmailTokenException();
            }
        }
    }
}
