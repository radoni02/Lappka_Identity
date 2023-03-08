using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Storage
{
    public interface IRequestStorage
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? duration = null);
    }
}
