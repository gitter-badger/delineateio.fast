using System.Collections;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Interface for the detailed command info
    /// </summary>
    public interface ICommandOptions : IEnumerable<CommandOption>, IEnumerable
    {
        /// <summary>
        /// Adds an option to the collection
        /// </summary>
        /// <param name="option">The option to add</param>
        void Add(ICommandOption option);

        /// <summary>
        /// Indoicates if a specific option is present
        /// </summary>
        /// <param name="key">The key of the option</param>
        /// <returns>Returns true if the option available</returns>
        bool Has(string key);
    }
}