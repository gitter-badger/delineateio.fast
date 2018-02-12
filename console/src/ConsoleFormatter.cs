using System;
using System.Text;
using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;

namespace Delineate.Fast
{
    /// <summary>
    /// ConsoleWriter provides additional formatting options 
    /// when writing output via the console
    /// </summary>
    public static class ConsoleFormatter
    {
        /// <summary>
        /// Writes a message event args to the console 
        /// </summary>
        /// <param name="e">The event args to write</param>
        public static void Write(MessageEventArgs e)
        {
            foreach(MessageInfo message in e.Messages)
            {
                Write(message);
            }
        }

        /// <summary>
        /// Writes the messasges from the commands
        /// </summary>
        /// <param name="message">Writes an individual message</param>
        public static void Write(MessageInfo message)
        {
            StringBuilder builder = new StringBuilder();
    
            for(int i = 0; i < message.Indent; i++)
            {
                if( i == message.Indent -1 && message.IsNested)
                    builder.Append("  - ");
                else
                    builder.Append("    ");
            }

                builder.Append(message.Text.ToString() + Environment.NewLine);
            
            Console.ForegroundColor = GetColor(message.Level);
            Console.Write(builder.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Writes exception detail to the output
        /// </summary>
        /// <param name="exception">The exception to write the detail for</param>
        public static void WriteException(Exception exception)
        {
            string[] values = new string[]
                                        {
                                            exception.Message,
                                            exception.Source,
                                            exception.StackTrace
                                        };

            foreach(string text in values)
            {
                Write(new MessageInfo()
                {
                    Text = text,
                    Level = MessageLevel.Error
                });
            }
        }

        /// <summary>
        /// Converts the generic framework message type to
        /// the UI specific ConSolColor for display 
        /// </summary>
        /// <param name="level">The level of the message</param>
        /// <returns>Return the right ConsoleColor</returns>
        private static ConsoleColor GetColor(MessageLevel level)
        {
            switch(level)
            {
                case MessageLevel.Important: //Magenta
                    return ConsoleColor.Magenta;

                case MessageLevel.Normal: //White
                    return ConsoleColor.White;

                case MessageLevel.Success: //Green 
                    return ConsoleColor.DarkGreen;

                case MessageLevel.Warning: //Yellow
                    return ConsoleColor.DarkYellow;

                case MessageLevel.Error: // Red
                    return ConsoleColor.DarkRed;
                
                case MessageLevel.Link:  // Dark Cyan 
                    return ConsoleColor.DarkCyan;

                case MessageLevel.Info:  //Grey
                    return ConsoleColor.DarkGray;

                default:
                    return ConsoleColor.White;
            }
        }
    }
}