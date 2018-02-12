using Delineate.Fast.Core.Help;

namespace Delineate.Fast.Core.Commands.Handlers
{
    /// <summary>
    /// Help handler for handling help requests (--help)
    /// </summary>
    public class HelpCommandHandler : BaseCommandHandler
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void Handle()
        {   
            HelpSections help = new HelpSections(Context);
            help.DisplaySections();
        }
    }
}