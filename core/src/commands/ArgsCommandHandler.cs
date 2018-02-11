using Delineate.Fast.Core.Help;

namespace Delineate.Fast.Core.Commands
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
            Command.Outputs.Blank();
            Command.Outputs.Error("There was an error parsing the provided arguments");

            HelpManager help = new HelpManager(Command);
            help.Output();
        }
    }
}