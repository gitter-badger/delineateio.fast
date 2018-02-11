using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Help
{
    /// <summary>
    /// Base class for help sections 
    /// </summary>
    public abstract class BaseHelpSection
    {
        /// <summary>
        /// By default sections are visible, this property
        /// should be overriden where logic determines
        /// if the section should bevisible
        /// </summary>
        /// <returns>Return is true if the section is visible</returns>
        protected virtual bool IsVisible
        {
            get{ return true; }
        }

        /// <summary>
        /// The message manager to use for notifications
        /// </summary>
        /// <returns>The message manager</returns>
        public MessageManager Messages { get; internal set; }

        /// <summary>
        /// The command for which the help section will be output
        /// </summary>
        /// <returns>The command</returns>
        public Command Command { get; internal set; }

        /// <summary>
        /// The header text to be used for 
        /// </summary>
        /// <returns>Returns the header for the section</returns>
        public string Header { get; internal set; }

        /// <summary>
        /// Adds the header and the detail 
        /// </summary>
        public void Add()
        {
            if( IsVisible )
            {
                AddHeader();
                
                Messages.Indent(2);
                AddDetail();
                Messages.Unindent(2);
            }
        }

        /// <summary>
        /// Adds a header for the help section
        /// </summary>
        /// <param name="line">The header to add</param>
        protected void AddHeader()
        {
            Messages.Indent();
            Messages.Blank();
            Messages.Normal(Header.ToUpper());
            Messages.Unindent();
            Messages.Blank();
        }

        /// <summary>
        /// The method to be implemented to output the section
        /// </summary>
        protected abstract void AddDetail();
    }
}