using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Management.Commands
{
    public record RemoveAdminRoleCommand(Guid id) : ICommand;
    
    
}
