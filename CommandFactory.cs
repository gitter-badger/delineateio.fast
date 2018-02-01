using System;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    public sealed class CommandFactory
    {
        public static Command Create(ProgramArgs programArgs)
        {
            SortedDictionary<string, Type> commands = GetCommands();
            return CreateCommand(commands, programArgs);
        }

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

        private static SortedDictionary<String, Type> GetCommands()
        {
            SortedDictionary<string, Type> commands = new SortedDictionary<string, Type>();
            commands.Add("init", typeof(InitCommand));
            commands.Add("clean", typeof(CleanCommand));
            return commands;
        }
    }
}

