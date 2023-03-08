using Application.Dto;
using Application.Exceptions.UserException;
using Application.User.Queries;
using Convey.CQRS.Queries;
using Core.Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.QueryHandlers.UserQueryHandler
{
    public class GetCurrentUserDataQueryHandler : IQueryHandler<GetCurrentUserDataQuery, UserDto>
    {
        private readonly DbSet<AppUser> _appUsers; 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCurrentUserDataQueryHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _appUsers = context.appUsers;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> HandleAsync(GetCurrentUserDataQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await _appUsers.FirstOrDefaultAsync(x => x.Id == query.UserId);
            if(user is null)
            {
                throw new UserNotFoundByIdException(query.UserId);
            }
            var profilePicture = user.ProfilePicture == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(user.ProfilePicture) ? null : user.ProfilePicture;
            //if(!string.IsNullOrWhiteSpace(profilePicture)&&(Guid.TryParse(profilePicture, out var fileId)))
            //{
            //    var request = _httpContextAccessor.HttpContext.Request;
            //    profilePicture = $"{request.Scheme}://{request.Host}/files/Storage/{fileId}";
            //}
            var userDto = new UserDto(user.Id, user.FirstName, user.LastName, user.UserName, user.Email);
            userDto.ProfilePicture = profilePicture;
            return userDto;
        }
    }
}
