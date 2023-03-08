using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
        {
            builder
                .AddCommandHandlers()
                .AddQueryHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher();
            return builder;
        }
    }
}
