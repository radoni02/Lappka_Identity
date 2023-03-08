using Application.Dto;
using Convey.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries
{
    public record GetCurrentUserDataQuery : IQuery<UserDto>
    {
        public Guid UserId { get; set; }
    }

}
