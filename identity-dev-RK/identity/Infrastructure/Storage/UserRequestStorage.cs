using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Storage
{
    public sealed class UserRequestStorage : IUserRequestStorage
    {
        private readonly IRequestStorage _requestStorage;

        public UserRequestStorage(IRequestStorage requestStorage)
        {
            _requestStorage = requestStorage;
        }

        private static string GetKey(Guid commandId) => $"token:{commandId}";

        public void SetToken(Guid commandId,string token)
        {
            _requestStorage.Set(GetKey(commandId),token);
        }

        public string GetToken(Guid commandId)
        {
            return _requestStorage.Get<string>(GetKey(commandId));
        }
    }
}
