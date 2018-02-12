using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

using Delineate.Fast.Core.Logging;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Command factory for locating the commands
    /// </summary>
    public static class CommandFactory
    {
        private static SortedDictionary<string, Command> Commands = new SortedDictionary<string, Command>();

        /// <summary>
        /// Creates a command based on the closest match from the program args
        /// </summary>
        /// <param name="programArgs">The programs args</param>
        /// <returns>An instance of the command to execute</returns>
        public static CommandContext Create(string[] programArgs)
        {
            CommandContext context = new CommandContext();
            context.Logs.Log("Command requested: " + string.Join(", ", programArgs)); 

            if(Commands == null || Commands.Count == 0)
                LoadCommands(context);

            SetCommand(context, programArgs);
            
            SetupContext(context, context.Command.GetType());
            context.Command.Context = context;

            return context;
        }

        /// <summary>
        /// Method to find the command and instantiate an instance
        /// </summary>
        /// <param name="programArgs">The program args to be used to find the command</param>
        /// <returns>Returns an instance of teh command</returns>
        private static void SetCommand(CommandContext context, string[] programArgs)
        {
            Command command = Commands["default"];
            IList<string> args = new List<string>();

            for(int i = 0; i < programArgs.Length; i++)
            {
                if(programArgs[i].StartsWith("-"))
                { 
                    context.Logs.Log("Found first option, exited: " + programArgs[i]);
                    break;
                }

                args.Add(programArgs[i]);
                string match = string.Join(":", args);

                if( Commands.ContainsKey(match))
                {   
                    command = Commands[match];
                    context.Logs.Log(command.GetType().FullName);
                }
            } 

            context.Command = command;
        }
        
        #region Attribute Loading

        #endregion

        private static DirectoryInfo GetTemplatesDirectory()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                "plugins");
            
            return new DirectoryInfo(path);
        }

        /// <summary>
        /// Loads the commands collection if required 
        /// </summary>
        /// <returns>Returns a colelction keyed to be matched against the program args</returns>
        private static void LoadCommands(CommandContext context)
        {
            context.Logs.Log("Loading commands ...");

            // Loads any command from Core
            LoadAssemblyCommands(context, Assembly.GetAssembly(typeof(CommandFactory)));

            DirectoryInfo[] templates = GetTemplatesDirectory().GetDirectories();

            foreach(DirectoryInfo template in templates)
            {
                context.Logs.Log("Template: " + template.Name);
                context.Logs.Log("Path: " + template.FullName);
            
                foreach(FileInfo file in template.GetFiles("*.dll"))
                {
                    LoadAssemblyCommands(context, Assembly.LoadFile(file.FullName));
                }
            }
        }

        /// <summary>
        /// Loads the command for a given 
        /// </summary>
        /// <param name="assembly"></param>
        private static void LoadAssemblyCommands(CommandContext context, Assembly assembly)
        {
            context.Logs.Log(assembly.FullName);

            foreach (Type type in assembly.GetTypes())
            {
                if( type.IsSubclassOf(typeof(Command)))
                {   
                    Command command = Activator.CreateInstance(type) as Command;
                    context.Logs.Log(command.GetType().FullName);

                    SetupContext(context, type);

                    Commands.Add(context.Info.Key, command);
                }
            }
        }

        private static void SetupContext(CommandContext context, Type type)
        {
            var attributes = type.GetCustomAttributes();
            
            //TODO: HACK!
            context.Options = new CommandOptions();
            context.Info = null;

            foreach(Attribute attribute in attributes)
            {   
                AddInfo(context, attribute as CommandInfo);
                AddOption(context, attribute as CommandOption);
            }
        }

        private static void AddInfo(CommandContext context, CommandInfo info)
        {
            if( info != null )
            {
                context.Info = info;
                context.Logs.Log(info.ToLog());
            } 
        }

        private static void AddOption(CommandContext context, CommandOption option)
        {
            if( option != null)
            {
                context.Options.Add(option);
                context.Logs.Log(option.ToLog());
            }
        }
    }
}

