using System;
using System.Collections.Generic;

namespace Delineate.Fast
{
    public abstract class CommandFactory
    {
        public static Command Create(CommandArgs args)
        {
            //Gets the list of commands
            SortedDictionary<CommandArgs, Type>  commands = GetCommands();

            Type foundCommand = null;

            //Searches 
            for(int i = 0; i < args.Values.Length; i++)
            {
                CommandArgs searchArgs = new CommandArgs(new string[]{"init"});
                Type currentType = commands[searchArgs];
                if( currentType == null) 
                    break;
                
                foundCommand = currentType;
            } 

            // Return the commands
            if (foundCommand == null )
            {
                foundCommand = typeof(NullCommand);
            }

            return Activator.CreateInstance(foundCommand) as Command;
        }

        public static SortedDictionary<CommandArgs, Type> GetCommands()
        {
            SortedDictionary<CommandArgs, Type> commands = new SortedDictionary<CommandArgs, Type>();
            CommandArgs args = new CommandArgs(new string[]{"init"});
            commands.Add(args, typeof(InitCommand));
            return commands;
        }
    }
}

