﻿using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands
{
    public record SetNewPasswordCommand(string Token,string Password ,string NewPassword, string Email) : ICommand;
}
