using Application.Exceptions.UserException;
using Application.Grpc;
using Application.User.Commands;
using Convey.CQRS.Commands;
using Core.Domain.Models;
using Core.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Auth.Handlers
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IIdentityRepository _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotificationGrpcClient _notificationGrpcClient;

        public RegisterCommandHandler(IIdentityRepository repo, UserManager<AppUser> userManager, INotificationGrpcClient notificationGrpcClient)
        {
            _repo = repo;
            _userManager = userManager;
            _notificationGrpcClient = notificationGrpcClient;
        }

        public async Task HandleAsync(RegisterCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            if(!command.Password.Equals(command.ConfirmPassword))
            {
                throw new PasswordMustBeSameException();
            }
            var userExist = await _repo.FindByEmailAsync(command.EmailAddress);
            if(userExist!=null)
            {
                throw new UserAlreadyExistException(userExist.Email);
            }
            var user = new AppUser(command.FirstName, command.LastName, command.EmailAddress);
            var result = await _userManager.CreateAsync(user,command.Password);  
            if(!result.Succeeded)
            {
                throw new UserCannotBeCreatedException();
            }
            //var descriptor = new SecurityTokenDescriptor
            //{
            //    Claims = new Dictionary<string, object>()
            //{
            //    { "email", user.Email }
            //}
            //};
            //var handler = new JwtSecurityTokenHandler();
            //var secToken = new JwtSecurityTokenHandler().CreateToken(descriptor);
            //var token = handler.WriteToken(secToken);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _notificationGrpcClient.SendConfirmEmailToConfirmEmail(command.EmailAddress, token, user.UserName, user.FirstName, user.LastName, user.Id);
            //Console.WriteLine("===============================================================================");
            //Console.WriteLine(token);
            //Console.WriteLine("===============================================================================");
        }
    }



}
