using Application.Auth.Commands;
using Application.Exceptions.TokenException;
using Application.Exceptions.UserException;
using Application.Grpc;
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

namespace Application.Auth.Handlers
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
    {
        private readonly IIdentityRepository _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotificationGrpcClient _notificationGrpcClient;

        public ResetPasswordCommandHandler(IIdentityRepository repo,UserManager<AppUser> userMenager, INotificationGrpcClient notificationGrpcClient)
        {
            _repo = repo;
            _userManager = userMenager;
            _notificationGrpcClient = notificationGrpcClient;
        }

        public async Task HandleAsync(ResetPasswordCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _repo.FindByEmailAsync(command.Email);
            if(user is null)
            {
                throw new UserNotFoundByEmailException(command.Email);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if(token is null)
            {
                throw new NullPasswordResetTokenException();
            }
            await _notificationGrpcClient.SendEmailToResetPassword(command.Email, token);
            var result = HttpUtility.UrlEncode(token);
        }

    }
}
