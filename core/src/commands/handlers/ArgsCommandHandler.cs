using Delineate.Fast.Core.Help;

namespace Delineate.Fast.Core.Commands.Handlers
{
    /// <summary>
    /// Args Error handler used when there are errors
    /// in the args that have been provided to the command
    /// </summary>
    public class ArgsCommandHandler : BaseCommandHandler
    {
        /// <summary>
        /// Writes the required errors to the output and displays 
        /// help as required 
        /// </summary>
        protected override void Handle()
        {
            Context.Messenger.Blank();
            Context.Messenger.Error("There was an error parsing the provided arguments");

            HelpSections help = new HelpSections(Context);
            help.DisplaySections();
        }
    }
}