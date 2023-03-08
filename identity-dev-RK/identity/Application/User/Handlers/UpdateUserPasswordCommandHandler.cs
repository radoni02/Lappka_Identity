using Application.Exceptions.UserException;
using Application.User.Commands;
using Convey.CQRS.Commands;
using Core.Domain.Models;
using Core.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Handlers
{
    public class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand>
    {
        private readonly IIdentityRepository _repo;
        private readonly SignInManager<AppUser> _signInManager;

        public UpdateUserPasswordCommandHandler(IIdentityRepository repo, SignInManager<AppUser> signInManager)
        {
            _repo = repo;
            _signInManager = signInManager;
        }

        public async Task HandleAsync(UpdateUserPasswordCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await _repo.FindByIdAsync(command.Id);
            if(user is null)
            {
                throw new UserNotFoundByIdException(command.Id);
            }
            user.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user, command.Password);
            await _repo.UpdateAsync(user);
        }
    }
}
