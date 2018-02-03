using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Delineate.Fast.Commands
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
                args.Add(programArgs[i]);
                string match = string.Join(":", args);

                if( ! Commands.ContainsKey(match))
                    break;
                
                commandType = Commands[match];
            } 

            return Activator.CreateInstance(commandType) as Command;
        }

        /// <summary>
        /// Loads the commands collection if required 
        /// </summary>
        /// <returns>Returns a colelction keyed to be matched against the program args</returns>
        private static SortedDictionary<String, Type> LoadCommands()
        {
            SortedDictionary<string, Type> commands = new SortedDictionary<string, Type>();
            
            //TODO: Need to ensure the dll is loaded into the App Domain
            //TODO: Refactor the code here
            
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) 
            {
                AssemblyName name = assembly.GetName();

                if( ! name.Name.StartsWith("System"))
                {               
                    foreach (Type type in assembly.GetTypes())
                    {
                        var attributes = type.GetCustomAttributes(typeof(CommandMatch), false);
                        if (attributes != null && attributes.Length > 1)
                        {
                            foreach(CommandMatch attribute in attributes)
                            {   
                                commands.Add(attribute.Key, type);
                            }
                        }
                    }
                }
            }

            //commands.Add("cloud:init", typeof(InitCommand));
            //commands.Add("cloud:run", typeof(RunCommand));
            //commands.Add("cloud:clean", typeof(CleanCommand));
            // commands.Add("clean", typeof(CleanCommand));
            return commands;
        }
    }
}

