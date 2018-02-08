using System;
using System.Text;
using System.Collections.Generic;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast
{
    /// <summary>
    /// ConsoleWriter provides additional formatting options 
    /// when writing output via the console
    /// </summary>
    public static class ProgramWriter
    {
        public static void Write(string line, ConsoleColor color = ConsoleColor.White, 
                                            int blanks = 0, int indent = 0)
        {
            
            List<string> lines = new List<string>();
            lines.Add(line);
            Write(lines, color, blanks, indent);
        }

        public static void Write(OutputEventArgs e)
        {
            Write(e.Lines, e.Color, e.Blanks, e.Indent);
        }

        /// <summary>
        /// Writes the messasges from the commands
        /// </summary>
        /// <param name="messages"></param>
        public static void Write(IList<string> lines, ConsoleColor color = ConsoleColor.White, 
                                            int blanks = 0, int indent = 0)
        {
            StringBuilder builder = new StringBuilder();

            while(blanks > 0)
            {
                Console.WriteLine(string.Empty);
                blanks--;
            }
                        
            foreach(var line in lines)
            {
                for(int i = 0; i < indent; i++)
                {
                    if( i == indent -1 )
                        builder.Append(" - ");
                    else
                        builder.Append("   ");
                }

                builder.Append(line.ToString() + Environment.NewLine);
            }

            Console.ForegroundColor = color;
            Console.Write(builder.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}