using Application.Exceptions.UserException;
using Application.Services;
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
using System.Web;

namespace Application.User.Handlers
{
    public class ConfirmUpdateUserEmailCommandHandler : ICommandHandler<ConfirmUpdateUserEmailCommand>
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly IIdentityRepository _repo;
        private readonly UserManager<AppUser> _userManager;

        public ConfirmUpdateUserEmailCommandHandler(IJwtHandler jwtHandler, IIdentityRepository repo, UserManager<AppUser> userManager)
        {
            _jwtHandler = jwtHandler;
            _repo = repo;
            _userManager = userManager;
        }

        public async Task HandleAsync(ConfirmUpdateUserEmailCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var token = HttpUtility.UrlDecode(command.Token);
            string email;
            var id = Guid.Empty;
            (email,id) =  _jwtHandler.ReadAccessTokenForEmail(command.Token);
            var user = await _repo.FindByIdAsync(id);
            if(user is null)
            {
                throw new UserNotFoundByIdException(id);
            }
            user.Email = email;
            user.NormalizedEmail = email.ToUpper();
            await _repo.UpdateAsync(user);
        }
    }
}
