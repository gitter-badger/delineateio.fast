using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;

namespace Delineate.Fast.Core.Help
{
    /// <summary>
    /// Implementation for the command help section
    /// </summary>
    public class CommandHelpSection : BaseHelpSection
    {
        /// <summary>
        /// Overrides the IsVisible default, this section 
        /// is not to be displaye for the Default command
        /// </summary>
        /// <returns>Returns true when the help section is to be shown</returns>
        protected override bool IsVisible
        {
            get{ return ! Context.Info.IsDefault; }
        }

        /// <summary>
        /// Displays the command section when required, for 
        /// more detail see the IsVisible documentation
        /// </summary>
        protected override void AddDetail()
        {
            string detail = Context.Info.Key.Replace(":", " ") + " [OPTIONS]";
            Context.Messenger.Success(detail);
        }
    }
}