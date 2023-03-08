
using Grpc.Net.Client;
using Grpc.Core; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Application.Grpc;
using Application.Exceptions.NotificationException;
using System.Net;

namespace Infrastructure.GrpcClient
{
    public class NotificationGrpcClient : INotificationGrpcClient
    {
        private readonly GrpcChannel _channel;
        private readonly NotificationService.NotificationServiceClient _notificationServiceClient;

        public NotificationGrpcClient(GrpcSettings grpcSettings/*, NotificationService.NotificationServiceClient notificationServiceClient*/)
        {
            _channel = GrpcChannel.ForAddress(grpcSettings.NotificationServerAddress);
            _notificationServiceClient = new NotificationService.NotificationServiceClient(_channel);
        }

        public async Task SendEmailToResetPassword(string email,string token)
        {
            await HandleEmailTask(_notificationServiceClient.ResetPasswordAsync( new ResetPasswordRequest()
            {
                Email = email,
                Token = HttpUtility.UrlEncode(token)
            }));

        }

        public async Task SendEmailToChangeEmail(string email,string token,string userId)
        {
            await HandleEmailTask(_notificationServiceClient.ChangeEmailAsync(new ChangeEmailRequest()
            {
                Email=email,
                Token = token,
                UserId = userId,
                
            }));

        }

        public async Task SendConfirmEmailToConfirmEmail(string email, string token, string userName, string FirstName, string LastName,Guid userId)
        {
            {
                await HandleEmailTask(_notificationServiceClient.ConfirmEmailAsync(new ConfirmEmailRequest()
                {
                    Email = email,
                    Token = token,
                    Username = userName,
                    Firstname = FirstName,
                    Lastname = LastName,
                    Userid = userId.ToString(),
                }));
            }
        }
        private async Task HandleEmailTask(AsyncUnaryCall<Google.Protobuf.WellKnownTypes.Empty> task)
        {
            try
            {
                await task;
            }
            catch (RpcException rex)
            {
                throw rex.StatusCode switch
                {
                    StatusCode.InvalidArgument => new FailToSendEmailException(rex.Status.Detail, rex, HttpStatusCode.BadRequest),
                    StatusCode.NotFound => new FailToSendEmailException(rex.Status.Detail, rex, HttpStatusCode.NotFound),
                    _ => new FailToSendEmailException(rex.Status.Detail, rex)
                };
            }
            catch (Exception ex)
            {
                throw new FailToSendEmailException(ex.Message, ex);
            }
        }
    }
}
