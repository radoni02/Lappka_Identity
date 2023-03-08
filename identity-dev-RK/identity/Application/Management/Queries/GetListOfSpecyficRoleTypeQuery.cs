using Application.Dto;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Management.Queries
{
    public record GetListOfSpecyficRoleTypeQuery(Role role ) : IQuery<List<UserDto>>;
    
}
