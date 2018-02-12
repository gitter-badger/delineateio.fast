using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;

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
        public ICommandContext Context { get; internal set; }

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
                
                Context.Messenger.Indent(2);
                AddDetail();
                Context.Messenger.Unindent(2);
            }
        }

        /// <summary>
        /// Adds a header for the help section
        /// </summary>
        /// <param name="line">The header to add</param>
        protected void AddHeader()
        {
            Context.Messenger.Indent();
            Context.Messenger.Blank();
            Context.Messenger.Normal(Header.ToUpper());
            Context.Messenger.Unindent();
            Context.Messenger.Blank();
        }

        /// <summary>
        /// The method to be implemented to output the section
        /// </summary>
        protected abstract void AddDetail();
    }
}