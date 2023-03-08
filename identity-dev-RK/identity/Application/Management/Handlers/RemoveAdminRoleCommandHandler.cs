using Application.Management.Commands;
using Convey.CQRS.Commands;
using Core.Domain.Models;
using Core.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Management.Handlers
{
    public class RemoveAdminRoleCommandHandler : ICommandHandler<RemoveAdminRoleCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityRepository _repo;

        public RemoveAdminRoleCommandHandler(UserManager<AppUser> userManager, IIdentityRepository repo)
        {
            _userManager = userManager;
            _repo = repo;
        }

        public async Task HandleAsync(RemoveAdminRoleCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await _repo.FindByIdAsync(command.id);
            if(user is null)
            {
                throw new Exception();
            }
            if(user.Role!=Role.Admin)
            {
                throw new Exception();
            }
            user.Role = Role.User;
            await _repo.UpdateAsync(user);
        }
    }
}
