using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Command factory for locating the commands
    /// </summary>
    public static class CommandFactory
    {
        private static SortedDictionary<string, Type> Commands = new SortedDictionary<string, Type>();

        /// <summary>
        /// Creates a command based on the closest match from the program args
        /// </summary>
        /// <param name="programArgs">The programs args</param>
        /// <returns>An instance of the command to execute</returns>
        public static Command Create(string[] programArgs)
        {
            if(Commands == null || Commands.Count == 0)
                LoadCommands();

            return CreateCommand(programArgs);
        }

        /// <summary>
        /// Method to find the command and instantiate an instance
        /// </summary>
        /// <param name="programArgs">The program args to be used to find the command</param>
        /// <returns>Returns an instance of teh command</returns>
        private static Command CreateCommand(string[] programArgs)
        {
            Type commandType = typeof(NullCommand);
            List<string> args = new List<string>();

            for(int i = 0; i < programArgs.Length; i++)
            {
                if(programArgs[i].StartsWith("-"))
                    break;

                args.Add(programArgs[i]);
                string match = string.Join(":", args);

                if( Commands.ContainsKey(match))
                    commandType = Commands[match];
            } 

            return Activator.CreateInstance(commandType) as Command;
        }

        /// <summary>
        /// Loads the commands collection if required 
        /// </summary>
        /// <returns>Returns a colelction keyed to be matched against the program args</returns>
        private static void  LoadCommands()
        {
            string path = string.Format("{0}{1}{2}",
                                            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                            Path.DirectorySeparatorChar,
                                            "commands");

            foreach(string file in Directory.GetFiles(path, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFile(file);

                foreach (Type type in assembly.GetTypes())
                {
                    var attributes = type.GetCustomAttributes(typeof(CommandMatch));
                    if (attributes != null)
                    {
                        foreach(CommandMatch attribute in attributes)
                        {   
                            Commands.Add(attribute.Key, type);
                        }
                    }
                }
            }
        }
    }
}

