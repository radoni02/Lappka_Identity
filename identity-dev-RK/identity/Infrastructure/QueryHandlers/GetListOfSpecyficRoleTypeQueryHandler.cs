 using Application.Dto;
using Application.Management.Queries;
using Convey.CQRS.Queries;
using Core.Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.QueryHandlers
{
    public class GetListOfSpecyficRoleTypeQueryHandler : IQueryHandler<GetListOfSpecyficRoleTypeQuery, List<UserDto>>
    {
        private readonly ApplicationDbContext _db;

        public GetListOfSpecyficRoleTypeQueryHandler(ApplicationDbContext context)
        {
            _db = context;
        }
        public async Task<List<UserDto>> HandleAsync(GetListOfSpecyficRoleTypeQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
           if(query.role == Role.SuperAdmin || query.role ==Role.User)
            {
                throw new Exception();
            }

            var users = _db.appUsers
                .Where(x => x.Role == query.role)
                .Select(y => new UserDto(y.Id, y.FirstName, y.LastName, y.UserName, y.Email)).ToList();
            return users;
        }
    }
}
