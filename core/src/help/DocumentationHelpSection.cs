using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;

namespace Delineate.Fast.Core.Help
{
    /// <summary>
    /// Concrete implementation for a deocmentation section 
    /// </summary>
    public class DocumentationHelpSection : BaseHelpSection
    {
        /// <summary>
        /// Displays the url to the online documentation 
        /// </summary>
        protected override void AddDetail()
        {
            string helpResource = string.Concat(
                                        "http://www.delineate.io/fast/",
                                        Context.Info.Key.Replace(":", "/"));

            Context.Messenger.Link(helpResource);
        }
    }
}