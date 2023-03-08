using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands
{
    public record ShelterRegisterCommand(string OrganizationName, double Longitude, double Latitude,
    string NIP, string KRS, string PhoneNumber) : ICommand;
   
}
