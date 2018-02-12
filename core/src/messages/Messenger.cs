using System.Collections.Generic;

namespace Delineate.Fast.Core.Messaging
{
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);

    /// <summary>
    /// Utility class for managing output
    /// </summary>
    public sealed class Messenger : IMessenger
    {

        /// <summary>
        /// Event raised when flushed 
        /// </summary>        
        public event MessageEventHandler OnFlushed;

        #region Properties

        /// <summary>
        /// The current indent
        /// </summary>
        /// <returns>Returns the current indent value</returns>
        public int CurrentIndent { get; private set; }

        /// <summary>
        /// Indicates if indents are nested
        /// </summary>
        /// <returns>Return true if nested</returns>
        public bool IsNested { get; private set; }

        /// <summary>
        /// Indicates if the output is set to quiet
        /// </summary>
        /// <returns></returns>
        public bool IsQuiet { get; set; }

        /// <summary>
        /// The current collection of unflushed events
        /// </summary>
        /// <returns></returns>
        private IList<MessageInfo> Messages {get; set;} 

        /// <summary>
        /// Indicates if output is to be autom flushed
        /// </summary>
        /// <returns></returns>
        public bool AutoFlush { get; set; }

        #endregion

        #region Indent and Unindent

        /// <summary>
        /// Increments the indent by one
        /// </summary>
        public void Indent() 
        {
            CurrentIndent++;
        }

        /// <summary>
        /// Incremenets the indent by the given number
        /// </summary>
        /// <param name="number">The number to incr4ement by</param>
        public void Indent(int number) 
        {
            CurrentIndent = CurrentIndent + number;
        }

        /// <summary>
        /// Unindents the output by one
        /// </summary>
        public void Unindent()
        {
            if(CurrentIndent > 0)
                CurrentIndent--;

            if(CurrentIndent < 0)
                CurrentIndent = 0;
        }

        /// <summary>
        /// Unindents by the specified number
        /// </summary>
        /// <param name="number">The number to unindent by</param>
        public void Unindent(int number)
        {
            CurrentIndent = CurrentIndent - number;

            if(CurrentIndent < 0)
                CurrentIndent = 0;
        }

        #endregion
        
        #region Nesting

        public void Nest()
        {
            IsNested = true;
        }

        public void Unnest()
        {
            IsNested = false;
        }

        #endregion

        #region Add

        /// <summary>
        /// Writes an normal message
        /// </summary>
        /// <param name="text">The message</param>
        public void Normal(string text)
        {
            Add(text, MessageLevel.Normal);
        }

        /// <summary>
        /// Writes a nomral message with additional
        /// formatted values
        /// </summary>
        /// <param name="text">The message</param>
        /// <param name="values">The values to format</param>
        public void Normal(string text, params string[] values  )
        {
            Add(string.Format(text, values), MessageLevel.Normal);
        }

        /// <summary>
        /// Writes an important message
        /// </summary>
        /// <param name="text">The message</param>
        public void Important(string text)
        {
            Add(text, MessageLevel.Important);
        }

        /// <summary>
        /// Writes an error message
        /// </summary>
        /// <param name="text">The message</param>
        public void Error(string text)
        {
            Add(text, MessageLevel.Error);
        }

        /// <summary>
        /// Writes an link message
        /// </summary>
        /// <param name="text">The message</param>
        public void Link(string text)
        {
            Add(text, MessageLevel.Link);
        }

        /// <summary>
        /// Writes an warning message
        /// </summary>
        /// <param name="text">The message</param>
        public void Warning(string text)
        {
            Add(text, MessageLevel.Warning);
        }

        /// <summary>
        /// Writes an warning message with additional
        /// formatted values
        /// </summary>
        /// <param name="text">The message</param>
        /// <param name="values">The values to format</param>
        public void Warning(string text, params string[] values  )
        {
            Add(string.Format(text, values), MessageLevel.Warning);
        }

        /// <summary>
        /// Writes an info message
        /// </summary>
        /// <param name="text">The message</param>
        public void Info(string text)
        {
            Add(text, MessageLevel.Info);
        }

        /// <summary>
        /// Writes a success message
        /// </summary>
        /// <param name="text">The message</param>
        public void Success(string text)
        {
            Add(text, MessageLevel.Success);
        }

        /// <summary>
        /// Writes a success message and formats the values into 
        /// the base message 
        /// </summary>
        /// <param name="text">The base message</param>
        /// <param name="values">The values to format</param>
        public void Success(string text, params string[] values  )
        {
            Add(string.Format(text, values), MessageLevel.Success);
        }

        /// <summary>
        /// Writes a blank line to the messenger
        /// </summary>
        public void Blank()
        {
            Add(string.Empty, MessageLevel.Normal);
        }

        #endregion

        /// <summary>
        /// Adds a message to the queue
        /// </summary>
        /// <param name="text">The text for the message</param>
        /// <param name="level">The message level</param>
        /// <remarks>
        /// If AUtoFlust is set to true then the message is 
        /// flushed immediately, if not it is flushed when the
        /// flush message is called
        /// <summary>
        private void Add(string text, MessageLevel level)
        {
            if(IsQuiet) return;
            
            if(Messages == null)
                Messages = new List<MessageInfo>();

            Messages.Add( new MessageInfo()
            {
                Text = text,
                Level = level,
                Indent = CurrentIndent,
                IsNested = this.IsNested
            });

            if(AutoFlush)
                Flush();
        }

        #region Flush
        
        /// <summary>
        /// Flushes messages that have to been returned to subscribers
        /// </summary>
        public void Flush()
        {
            MessageEventHandler handler = OnFlushed;   

            if (handler != null) 
            {
                MessageEventArgs e = new MessageEventArgs()
                {
                    Messages = this.Messages
                };

                handler(this, e);
            }

            Messages.Clear();
        }

        #endregion
    }
}
