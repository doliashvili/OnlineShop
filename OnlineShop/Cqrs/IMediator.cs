using System.Threading.Tasks;
using CoreModels.Messaging;

namespace Cqrs
{
    public interface IMediator
    {
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query);
        Task SendAsync<T>(T command) where T: class, ICommand;
        Task PublishAsync<T>(T @event) where T: IEvent;
    }
}