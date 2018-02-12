using System.Collections.Generic;

namespace Delineate.Fast.Core.Messaging
{
    /// <summary>
    /// Interface for class that can act as messengers
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// The OnFlushed event is used to raise events
        /// to subscribers when the messages are flushed
        /// </summary>
        event MessageEventHandler OnFlushed;

        /// <summary>
        /// The current indent
        /// </summary>
        /// <returns>Returns the current indent value</returns>
        int CurrentIndent { get; }

        /// <summary>
        /// Indicates if indents are nested
        /// </summary>
        /// <returns>Return true if nested</returns>
        bool IsNested { get; }

        /// <summary>
        /// Indicates if the output is set to quiet
        /// </summary>
        /// <returns></returns>
        bool IsQuiet { get; set; }

        /// <summary>
        /// Indicates if output is to be autom flushed
        /// </summary>
        /// <returns></returns>
        bool AutoFlush { get; set; }

        /// <summary>
        /// Indents the messages by one
        /// </summary>
        void Indent();

        /// <summary>
        /// Incremenets the indent by the given number
        /// </summary>
        /// <param name="number">The number to incr4ement by</param>
        void Indent(int number);

        /// <summary>
        /// Unindents the output by one
        /// </summary>
        void Unindent();

        /// <summary>
        /// Unindents by the specified number
        /// </summary>
        /// <param name="number">The number to unindent by</param>
        void Unindent(int number);

        /// <summary>
        /// Nests indented messages
        /// </summary>
        void Nest();

        /// <summary>
        /// Stops nesting indents
        /// </summary>
        void Unnest();

        /// <summary>
        /// Writes a nomral message
        /// </summary>
        /// <param name="text">The message</param>
        void Normal(string text);

        /// <summary>
        /// Writes a nomral message with additional
        /// formatted values
        /// </summary>
        /// <param name="text">The message</param>
        /// <param name="values">The values to format</param>
        void Normal(string text, params string[] values  );

        /// <summary>
        /// Writes an important message
        /// </summary>
        /// <param name="text">The message</param>
        void Important(string text);

        /// <summary>
        /// Writes an error message
        /// </summary>
        /// <param name="text">The message</param>
        void Error(string text);

        /// <summary>
        /// Writes an link message
        /// </summary>
        /// <param name="text">The message</param>
        void Link(string text);
        
        /// <summary>
        /// Writes a warning message
        /// </summary>
        /// <param name="text">The message</param>
        void Warning(string text);

        /// <summary>
        /// Writes an warning message with additional
        /// formatted values
        /// </summary>
        /// <param name="text">The message</param>
        /// <param name="values">The values to format</param>
        void Warning(string text, params string[] values);

        /// <summary>
        /// Writes an info message
        /// </summary>
        /// <param name="text">The message</param>
        void Info(string text);

        /// <summary>
        /// Writes a success message
        /// </summary>
        /// <param name="text">The message</param>
        void Success(string text);

        /// <summary>
        /// Writes a success message and formats the values into 
        /// the base message 
        /// </summary>
        /// <param name="text">The base message</param>
        /// <param name="values">The values to format</param>
        void Success(string text, params string[] values);

        /// <summary>
        /// Writes a blank line to the messenger
        /// </summary>
        void Blank();

        /// <summary>
        /// Flushes messages that have to been returned to subscribers
        /// </summary>
        void Flush();
    }
}