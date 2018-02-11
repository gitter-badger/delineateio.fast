using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Core.Help
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
            HelpManager help = new HelpManager(Context);
            help.Output();
        }
    }
}