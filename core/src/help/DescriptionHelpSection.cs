using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;

namespace Delineate.Fast.Core.Help
{
    /// <summary>
    /// Concrete implementation of the documentation help section
    /// </summary>
    public class DescriptionHelpSection : BaseHelpSection
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
        /// Adds the description detail when required (See IsVisible)
        /// </summary>
        protected override void AddDetail()
        {
            Context.Messenger.Normal(Context.Info.Description);
        }
    }
}