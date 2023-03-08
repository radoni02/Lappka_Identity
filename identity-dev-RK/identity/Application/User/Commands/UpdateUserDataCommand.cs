using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands
{
    public record UpdateUserDataCommand(Guid Id,string FirstName,string LastName,string ProfilePicture) : ICommand;
}
