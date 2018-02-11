using System.Collections.Generic;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Core.Help
{
    public class HelpManager
    {
        private List<BaseHelpSection> Sections {get; set;}

        public HelpManager(CommandContext context)
        {
            LoadSections(context);
        }

        private void LoadSections(CommandContext context)
        {
            Sections = new List<BaseHelpSection>();
            Sections.Add( Create<PluginHelpSection>(context, "Plugin") );
            Sections.Add( Create<CommandHelpSection>(context, "Command") );
            Sections.Add( Create<DescriptionHelpSection>(context, "Description") );
            Sections.Add( Create<OptionsHelpSection>(context, "Options") );
            Sections.Add( Create<DocumentationHelpSection>(context, "Documentation") );
            Sections.Add( Create<VersionsHelpSection>(context, "Versions") );
        }

        private T Create<T>(CommandContext context, string header) where T: BaseHelpSection, new()
        {
            return new T()
            {
                Header = header,
                Context = context
            };
        }

        public void Output()
        {
            foreach(BaseHelpSection section in Sections)
            {
                section.Add();
            }
        }
    }
}