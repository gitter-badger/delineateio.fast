using System;
using System.Text;
using System.Collections;

namespace Delineate.Fast.Commands
{
    /// <summary>
    /// ConsoleWriter provides additional formatting options 
    /// when writing output via the console
    /// </summary>
    public static class ConsoleWriter
    {
        /// <summary>
        /// Method enables the writing of a single line
        /// </summary>
        /// <param name="line">Text to be written to the line</param>
        /// <param name="color">The colour to be used when writing the line</param>
        /// <param name="indent">The indent position when writing the line</param>
        /// <param name="isNested">Indicates if nested formatting should be applied when indenting</param>
        /// <param name="blank">The number of blank lines to write beforehand</param>
        public static void WriteLine(string line, ConsoleColor color = ConsoleColor.White, 
                                        int indent = 0, bool isNested = false, int blank = 0)
        {
            WriteLines(new []{line}, color, indent, isNested, blank);
        }

        /// <summary>
        /// Method enables the writing of multiple lines
        /// </summary>
        /// <param name="lines">The lines to be written</param>
        /// <param name="color">The colour to be used when writing the lines</param>
        /// <param name="indent">The indent position when writing the lines</param>
        /// <param name="isNested">Indicates if nested formatting should be applied when indenting</param>
        /// <param name="blank">The number of blank lines to write beforehand</param>
        public static void WriteLines(IEnumerable lines,  ConsoleColor color = ConsoleColor.White, int indent = 0, bool isNested = false, int blank = 0)
        {
            StringBuilder builder = new StringBuilder();

            while(blank > 0)
            {
                Console.WriteLine(string.Empty);
                blank--;
            }
                        
            foreach(var line in lines)
            {
                for(int i = 0; i < indent; i++)
                {
                    builder.Append(String.Format("  {0} ", 
                            isNested && i == indent - 1 ? "-" : " "));
                }

                builder.Append(line.ToString() + Environment.NewLine);
            }

            Console.ForegroundColor = color;
            Console.Write(builder.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}