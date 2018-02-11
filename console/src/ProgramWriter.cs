using System;
using System.Text;
using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast
{
    /// <summary>
    /// ConsoleWriter provides additional formatting options 
    /// when writing output via the console
    /// </summary>
    public static class ConsoleFormatter
    {
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
        /// <param name="messages"></param>
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

        public static void WriteException(Exception exception)
        {
            Write(new MessageInfo()
            {
                Text = exception.Message,
                Level = MessageLevel.Error
            });
        }

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