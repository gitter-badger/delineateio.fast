namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Interface for the detailed command info
    /// </summary>
    public interface ICommandInfo
    {
         /// <summary>
        /// The Key to be used when adding to the collection 
        /// </summary>
        /// <returns>Returns the key as a string</returns>
        string Key{ get; set;}

        /// <summary>
        /// Returns the description 
        /// </summary>
        /// <returns>The description of the command</returns>
        string Description { get; set; }

        /// <summary>
        /// Indicates that a command is core 
        /// </summary>
        /// <return>Returns true if the command is c ore</returns>
        bool IsCore { get; set; }

        /// <summary>
        /// Indicates that the command is the the default command 
        /// </summary>
        /// <returns></returns>
        bool IsDefault { get; set; }

        /// <summary>
        /// The plugin name is returned
        /// </summary>
        /// <returns>The plugin name</returns>
        string PluginName { get; set; }
    }
}