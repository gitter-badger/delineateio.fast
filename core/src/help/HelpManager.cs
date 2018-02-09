using System.Collections.Generic;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Core.Help
{
    public class HelpManager
    {
        public Command Command { get; set; }

        private List<BaseBuilder> Builders {get; set;}

        public HelpManager(Command command)
        {
            Command = command;
            LoadBuilders();
        }

        private void LoadBuilders()
        {
            Builders = new List<BaseBuilder>();
            Builders.Add( new PluginBuilder(Command));
            Builders.Add( new CommandBuilder(Command));
            Builders.Add( new DescriptionBuilder(Command));
            Builders.Add( new OptionsBuilder(Command));
            Builders.Add( new DocumentationBuilder(Command));
            Builders.Add( new VersionsBuilder(Command));
        }

        public void Output()
        {
            foreach(BaseBuilder builder in Builders)
            {
                builder.Output();
            }
        }
    }
}