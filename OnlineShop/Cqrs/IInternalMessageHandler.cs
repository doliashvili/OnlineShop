using System.Threading.Tasks;
using CoreModels.Messaging;

namespace Cqrs
{
    public interface IInternalEventHandler<in T> where T: IEvent
    {
        Task HandleAsync(T @event);
    }
}