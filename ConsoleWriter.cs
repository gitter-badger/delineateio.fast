using System;
using System.Text;
using System.Collections;

namespace Delineate.Cloud.Fast
{
    public static class ConsoleWriter
    {
        public static void WriteBlankLine()
        {
            Console.WriteLine(string.Empty);
        } 

        public static void WriteLine(string line, ConsoleColor color = ConsoleColor.White, int indent = 0, bool isNested = false)
        {
            WriteLines(new []{line}, color, indent, isNested);
        }

        public static void WriteLines(IEnumerable lines,  ConsoleColor color = ConsoleColor.White, int indent = 0, bool isNested = false)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            
            StringBuilder builder = new StringBuilder();

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
            Console.ForegroundColor = currentColor;
        }
    }
}