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
    public class UpdateUserDataCommandHandler : ICommandHandler<UpdateUserDataCommand>
    {
        private readonly IIdentityRepository _repo;
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserDataCommandHandler(IIdentityRepository repo, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task HandleAsync(UpdateUserDataCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await _repo.FindByIdAsync(command.Id);
            if(user is null)
            {
                throw new UserNotFoundByIdException(command.Id);
            }
            user.UpdateUserData(command.FirstName, command.LastName, command.ProfilePicture);
            await _repo.UpdateAsync(user);
        }
    }
}
