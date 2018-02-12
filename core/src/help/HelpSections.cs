using System.Collections.Generic;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Core.Help
{
    /// <summary>
    /// The Help Sections class is an implementation 
    /// of the builder pattern to displat the relevant 
    /// help sections for a command 
    /// </summary>
    public sealed class HelpSections
    {
        /// <summary>
        /// The collection that holds the section 
        /// builders 
        /// </summary>
        /// <returns></returns>
        private IList<BaseHelpSection> Sections {get; set;}

        /// <summary>
        /// Constructor takes the current execution context
        /// </summary>
        /// <param name="context">The current context</param>
        public HelpSections(ICommandContext context)
        {
            LoadSections(context);
        }

        /// <summary>
        /// Adds all the help sections in the appropriate sequential order
        /// </summary>
        /// <param name="context">Current execution context</param>        
        private void LoadSections(ICommandContext context)
        {
            Sections = new List<BaseHelpSection>();
            Sections.Add( Create<PluginHelpSection>(context, "Plugin") );
            Sections.Add( Create<CommandHelpSection>(context, "Command") );
            Sections.Add( Create<DescriptionHelpSection>(context, "Description") );
            Sections.Add( Create<OptionsHelpSection>(context, "Options") );
            Sections.Add( Create<DocumentationHelpSection>(context, "Documentation") );
            Sections.Add( Create<VersionsHelpSection>(context, "Versions") );
        }

        /// <summary>
        /// Creates an instance of the required help section object
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <param name="header">The header to use</param>
        /// <returns>The newly instantiated help section</returns>
        private T Create<T>(ICommandContext context, string header) where T: BaseHelpSection, new()
        {
            return new T()
            {
                Header = header,
                Context = context
            };
        }

        /// <summary>
        /// Displays the help sections by formatting the 
        /// help messages 
        /// </summary>
        public void DisplaySections()
        {
            foreach(BaseHelpSection section in Sections)
            {
                section.Add();
            }
        }
    }
}