using Application.Exceptions.UserException;
using Application.Grpc;
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
    public class UpdateUserEmailCommandHandler : ICommandHandler<UpdateUserEmailCommand>
    {
        private readonly IIdentityRepository _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtHandler _jwtHandler;
        private readonly INotificationGrpcClient _notificationGrpcClient;
        public UpdateUserEmailCommandHandler(IIdentityRepository repo, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager,
            IJwtHandler jwtHandler, INotificationGrpcClient notificationGrpcClient)
        {
            _repo = repo;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _notificationGrpcClient = notificationGrpcClient;
        }

        public async Task HandleAsync(UpdateUserEmailCommand command, CancellationToken cancellationToken = default)
        {
            if (!await _repo.CheckUserEmailAvailability(command.AdressEmail))
            {
                throw new Exception();  //emailalreadyexistException 
            }
            var user = await _repo.FindByIdAsync(command.Id);
            if(user is null)
            {
                throw new UserNotFoundByIdException(command.Id);
            }
            var token = _jwtHandler.GenerateConfirmUpdateEmailToken(user.Id, command.AdressEmail);
            await _notificationGrpcClient.SendEmailToChangeEmail(command.AdressEmail, token, command.Id.ToString());
            //var result = HttpUtility.UrlEncode(token);
            //Console.WriteLine("////////////////////////////////////////");
            //Console.WriteLine(token);
            //Console.WriteLine("////////////////////////////////////////");
        }
    }
}
