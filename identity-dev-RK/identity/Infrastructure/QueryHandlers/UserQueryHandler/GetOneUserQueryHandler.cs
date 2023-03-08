using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Exceptions.UserException;
using Application.User.Queries;
using Convey.CQRS.Queries;
using Core.Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.QueryHandlers.UserQueryHandler
{
    public class GetOneUserQueryHandler : IQueryHandler<GetOneUserQuery, UserDto>
    {
        private readonly DbSet<AppUser> _appUsers;

        public GetOneUserQueryHandler(ApplicationDbContext context)
        {
            _appUsers = context.Users;
        }

        public async Task<UserDto> HandleAsync(GetOneUserQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await _appUsers.FirstOrDefaultAsync(x => x.Id == query.UserId);
            if (user is null)
            {
                throw new UserNotFoundByIdException(query.UserId);
            }
            var newUser = new UserDto(user.Id, user.FirstName, user.LastName, user.UserName, user.Email);
            return newUser;
        }
    }
}
