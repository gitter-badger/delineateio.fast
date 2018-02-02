using System;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    /// <summary>
    /// Command factory for locating the commands
    /// </summary>
    public static class CommandFactory
    {
        /// <summary>
        /// Creates a command based on the closest match from the program args
        /// </summary>
        /// <param name="programArgs">The programs args</param>
        /// <returns>An instance of the command to execute</returns>
        public static Command Create(ProgramArgs programArgs)
        {
            SortedDictionary<string, Type> commands = GetCommands();
            return CreateCommand(commands, programArgs);
        }

        /// <summary>
        /// Method to find the command and instantiate an instance
        /// </summary>
        /// <param name="commands">The collection of available commands</param>
        /// <param name="programArgs">The program args to be used to find the command</param>
        /// <returns>Returns an instance of teh command</returns>
        private static Command CreateCommand(SortedDictionary<string, Type> commands, ProgramArgs programArgs)
        {
            Type commandType = typeof(NullCommand);
            List<string> args = new List<string>();

            for(int i = 0; i < programArgs.Values.Length; i++)
            {   
                args.Add(programArgs.Values[i]);
                string match = string.Join(ProgramArgs.SEPERATOR, args);

                if( ! commands.ContainsKey(match))
                    break;
                
                commandType = commands[match];
            } 

            return Activator.CreateInstance(commandType) as Command;
        }

        /// <summary>
        /// Gets the commands collection 
        /// </summary>
        /// <returns>Returns a colelction keyed to be matched against the program args</returns>
        private static SortedDictionary<String, Type> GetCommands()
        {
            //TODO: Dymaically load and cache the commands

            SortedDictionary<string, Type> commands = new SortedDictionary<string, Type>();
            commands.Add("init", typeof(InitCommand));
            commands.Add("run", typeof(RunCommand));
            commands.Add("vm:add", typeof(NullCommand));
            commands.Add("vm:remove", typeof(NullCommand));
            commands.Add("vm:list", typeof(NullCommand));
            commands.Add("clean", typeof(CleanCommand));
            return commands;
        }
    }
}

