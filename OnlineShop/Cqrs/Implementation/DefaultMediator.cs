using System;
using System.Linq;
using System.Threading.Tasks;
using CoreModels.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Implementation
{
    public class DefaultMediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        public DefaultMediator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));
            
            dynamic service =
                _serviceProvider.GetService(
                    typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse)));
            // ReSharper disable once PossibleNullReferenceException
            return await (Task<TResponse>) service.HandleAsync((dynamic) query);
        }

        public async Task SendAsync<T>(T command)
            where T : class, ICommand
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));
            
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }

        public async Task PublishAsync<T>(T @event) where T : IEvent
        {
            if (@event is null)
                throw new ArgumentNullException(nameof(@event));
            
            var handlers = _serviceProvider.GetServices<IInternalEventHandler<T>>();
            var tasks = handlers.Select(x => x.HandleAsync(@event));
            await Task.WhenAll(tasks);
        }
       
    }
}