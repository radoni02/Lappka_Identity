using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Grpc
{
    public interface INotificationGrpcClient
    {
        Task SendEmailToResetPassword(string email, string token);
        Task SendEmailToChangeEmail(string email, string token, string userId);
        Task SendConfirmEmailToConfirmEmail(string email, string token, string userName, string FirstName, string LastName, Guid userId);
    }
}
