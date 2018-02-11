using System.Collections.Generic;

namespace Delineate.Fast.Core.Messages
{
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);

    /// <summary>
    /// Utility class for managing output
    /// </summary>
    public sealed class MessageManager
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
        private List<MessageInfo> Messages {get; set;} 

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
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="isNested"></param>
        public void Normal(string line)
        {
            Add(line, MessageLevel.Normal);
        }

        public void Important(string line)
        {
            Add(line, MessageLevel.Important);
        }

        public void Error(string line)
        {
            Add(line, MessageLevel.Error);
        }

        public void Link(string line)
        {
            Add(line, MessageLevel.Link);
        }
    
        public void Warning(string line)
        {
            Add(line, MessageLevel.Warning);
        }

        public void Info(string line)
        {
            Add(line, MessageLevel.Info);
        }

        public void Success(string line)
        {
            Add(line, MessageLevel.Success);
        }

        public void Blank()
        {
            Add(string.Empty, MessageLevel.Normal);
        }

        #endregion

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
