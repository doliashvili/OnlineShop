using System.Threading.Tasks;
using CoreModels.Messaging;

namespace Cqrs
{
    /// <summary>
    /// Async handler for commands 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand> 
        where TCommand: ICommand
    {
        /// <summary>
        /// Handle command
        /// </summary>
        /// <param name="command">ICommand type</param>
        /// <returns>Task</returns>
        Task HandleAsync(TCommand command);
    }
}