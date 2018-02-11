using System.Collections.Generic;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Core.Help
{
    public class HelpManager
    {
        public Command Command { get; set; }

        private List<BaseHelpSection> Builders {get; set;}

        public HelpManager(Command command)
        {
            Command = command;
            LoadBuilders();
        }

        private void LoadBuilders()
        {
            Builders = new List<BaseHelpSection>();
            Builders.Add( Create<PluginHelpSection>("Plugin") );
            Builders.Add( Create<CommandHelpSection>("Command") );
            Builders.Add( Create<DescriptionHelpSection>("Description") );
            Builders.Add( Create<OptionsHelpSection>("Options") );
            Builders.Add( Create<DocumentationHelpSection>("Documentation") );
            Builders.Add( Create<VersionsHelpSection>("Versions") );
        }

        private T Create<T>(string header) where T: BaseHelpSection, new()
        {
            return new T()
            {
                Header = header,
                Command = Command,
                Messages = Command.Outputs
            };
        }

        public void Output()
        {
            foreach(BaseHelpSection builder in Builders)
            {
                builder.Add();
            }
        }
    }
}