using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cqrs.ApiGenerator;
using Cqrs.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cqrs.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static ApiGenOptions _apiGenOptions;
        private static ApiGen _apiGen;
        /// <summary>
        /// Add mediator to DI container
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>Service collection</returns>
        /// <exception cref="InvalidOperationException">param: assemblies can't be empty</exception>
        public static IServiceCollection AddMediator(this IServiceCollection services, ApiGenOptions apiGenOptions = default, params Assembly[] assemblies)
        {
            if(assemblies.Length == 0)
                throw new InvalidOperationException("[CQRS INSTALLER] assemblies to scan can not be empty");

            _apiGenOptions = apiGenOptions;
            if(apiGenOptions != null)
                _apiGen = new ApiGen(_apiGenOptions);
            
            services.AddScoped<IMediator, DefaultMediator>();
            services.AddInternalEventHandlers(assemblies);
            services.AddCommandHandlers(assemblies);
            services.AddQueryHandlers(assemblies);

            _apiGen?.GenerateControllerFiles();
            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            var commandInterfaceType = typeof(ICommandHandler<>);
            var handlers
                = FindHandlersByGenericInterface(commandInterfaceType, assemblies).ToList();

            if (_apiGenOptions != null)
            {
                _apiGen.RegisterCommandEndpoints(handlers);
            }
            
            foreach (var handler in handlers)
            {
                var genericInterfaceTypes = handler.GetInterfaces()
                    .Where(x => x.IsGenericType 
                                && x.GetGenericTypeDefinition() == commandInterfaceType).ToList();

                genericInterfaceTypes.ForEach(i => services.TryAddTransient(i, handler));
            }

            return services;
        }

        private static IServiceCollection AddInternalEventHandlers(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            var handlerInterfaceType = typeof(IInternalEventHandler<>);
            var handlers
                = FindHandlersByGenericInterface(handlerInterfaceType, assemblies).ToList();
            
            foreach (var handler in handlers)
            {
                var genericInterfaceTypes = handler.GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == handlerInterfaceType).ToList();

                genericInterfaceTypes.ForEach(i => services.AddTransient(i, handler));
            }

            return services;
        }
        
        private static IServiceCollection AddQueryHandlers(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            var queryHandlerInterfaceType = typeof(IQueryHandler<,>);

            foreach (var assembly in assemblies)
            {
                var handlerTypes = assembly.GetTypes()
                    .Where(x => x.IsClass && !x.IsAbstract
                                          && x.GetInterfaces()
                                              .Any(i => i.IsGenericType
                                                        && i.GetGenericTypeDefinition() == queryHandlerInterfaceType))
                    .ToList();

                if (_apiGenOptions != null)
                {
                    _apiGen.RegisterQueryEndpoints(handlerTypes);
                }
                
                foreach (var handlerType in handlerTypes)
                {
                    var genericInterfaces = handlerType.GetInterfaces()
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == queryHandlerInterfaceType).ToList();

                    genericInterfaces.ForEach(i => services.TryAddTransient(i, handlerType));
                }
            }
            
            return services;
        }
        
        
        private static IEnumerable<Type> FindHandlersByGenericInterface
            (Type genericInterfaceType, params Assembly[] assemblies)
        {
            var typeList = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var handlers = assembly.GetTypes()
                    .Where(t => t.IsClass
                                && !t.IsAbstract
                                && t.GetInterfaces()
                                    .Any(x => x.IsGenericType
                                              && x.GetGenericTypeDefinition() == genericInterfaceType));
                typeList.AddRange(handlers);
            }

            return typeList;
        }
    }
}