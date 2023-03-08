using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands
{
    public record RegisterCommand(string FirstName, string LastName, string EmailAddress, string Password,
        string ConfirmPassword) :ICommand;
    
}
