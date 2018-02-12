namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Interface for the detailed command info
    /// </summary>
    public interface ICommandOption
    {
        /// <summary>
        /// The primary key for the option 
        /// </summary>
        /// <returns>Returns the primary switch</returns>
        string Key { get; set; }
        
        /// <summary>
        /// Description of the option, output when the -h, --help 
        /// option is used for the command 
        /// </summary>
        /// <returns>Returns a user friendly description</returns>
        string Description { get; set; }
        
        /// <summary>
        /// Comma seperated list of aliases of the option
        /// </summary>
        /// <returns>Returns the list as strings</returns>
        string Aliases { get; set; }

        /// <summary>
        /// Indicates if the option must have a value supplied 
        /// </summary>
        /// <returns>Returns true if a value must be provided with the option</returns>
        bool HasValue { get; set; }
    }
}