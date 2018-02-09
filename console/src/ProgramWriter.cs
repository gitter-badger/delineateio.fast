using System;
using System.Text;
using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast
{
    /// <summary>
    /// ConsoleWriter provides additional formatting options 
    /// when writing output via the console
    /// </summary>
    public static class ProgramWriter
    {
        public static void Write(string line, OutputLevel level = OutputLevel.Normal, 
                                            int blanks = 0, int indent = 0)
        {
            
            List<string> lines = new List<string>();
            lines.Add(line);
            Write(lines, level, blanks, indent);
        }

        public static void Write(OutputEventArgs e)
        {
            Write(e.Lines, e.Level, e.Blanks, e.Indent, e.IsNested);
        }

        /// <summary>
        /// Writes the messasges from the commands
        /// </summary>
        /// <param name="messages"></param>
        public static void Write(IList<string> lines, OutputLevel level = OutputLevel.Normal, 
                                            int blanks = 0, int indent = 0, bool isNested = true)
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
                    if( i == indent -1 && isNested)
                        builder.Append("  - ");
                    else
                        builder.Append("    ");
                }

                builder.Append(line.ToString() + Environment.NewLine);
            }

            Console.ForegroundColor = GetColor(level);
            Console.Write(builder.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteException(Exception exception)
        {
            //TODO: Write to an error file
        }

        private static ConsoleColor GetColor(OutputLevel level)
        {
            switch(level)
            {
                case OutputLevel.Important: //Magenta
                    return ConsoleColor.Magenta;

                case OutputLevel.Normal: //White
                    return ConsoleColor.White;

                case OutputLevel.Success: //Green 
                    return ConsoleColor.DarkGreen;

                case OutputLevel.Warning: //Yellow
                    return ConsoleColor.DarkYellow;

                case OutputLevel.Error: // Red
                    return ConsoleColor.DarkRed;
                
                case OutputLevel.Link:  // Dark Cyan 
                    return ConsoleColor.DarkCyan;

                case OutputLevel.Info:  //Grey
                    return ConsoleColor.DarkGray;

                default:
                    return ConsoleColor.White;
            }
        }
    }
}