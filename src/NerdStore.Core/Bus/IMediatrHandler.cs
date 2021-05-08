using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Bus
{

    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T domainEvent) where T : Event;
    }
}
