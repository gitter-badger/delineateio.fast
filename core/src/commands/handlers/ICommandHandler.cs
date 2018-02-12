namespace Delineate.Fast.Core.Commands.Handlers
{
    /// <summary>
    /// Interface implemented by command handlers
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Execute method to be implemented 
        /// by specialist handlers
        /// </summary>
         void Execute(); 
    }
}