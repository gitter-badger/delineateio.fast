using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;

namespace Delineate.Fast.Core.Help
{
    /// <summary>
    /// Concrete implementation for showing the Plugin detail section
    /// </summary>
    public class PluginHelpSection : BaseHelpSection
    {
        /// <summary>
        /// Overrides the base visibility with a custom condition.
        /// The plugin section will only be present when the command
        /// is not a core command
        /// </summary>
        /// <returns>Returns true if the section is to be visible</returns>
        protected override bool IsVisible
        {
            get{ return ! Context.Info.IsCore; }
        }

        /// <summary>
        /// Displays the plugin name when required (see IsVisible)
        /// </summary>
        protected override void AddDetail()
        {
            Context.Messenger.Normal(Context.Info.PluginName);
        }
    }
}