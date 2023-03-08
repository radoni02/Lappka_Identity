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
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IIdentityRepository _repo;
        private readonly UserManager<AppUser> _userManager;

        public DeleteUserCommandHandler(IIdentityRepository repo, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await _repo.FindByIdAsync(command.Id);
            if(user is null)
            {
                throw new UserNotFoundByIdException(command.Id);
            }
            await _userManager.DeleteAsync(user);

        }
    }
}
