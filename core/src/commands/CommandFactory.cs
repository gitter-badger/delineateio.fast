using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

using Delineate.Fast.Core.Diagnostics;
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
        public static Command Create(string[] programArgs)
        {
            Debug.Indent();
            Debug.WriteLine("Command requested: " + string.Join(", ", programArgs));

            if(Commands == null || Commands.Count == 0)
                LoadCommands();

            Command command = GetCommand(programArgs);
            Debug.Unindent();

            return command; 
        }

        /// <summary>
        /// Method to find the command and instantiate an instance
        /// </summary>
        /// <param name="programArgs">The program args to be used to find the command</param>
        /// <returns>Returns an instance of teh command</returns>
        private static Command GetCommand(string[] programArgs)
        {
            Command command = Commands["default"];

            List<string> args = new List<string>();

            Debug.WriteLine("Search command:");
            Debug.Indent();

            for(int i = 0; i < programArgs.Length; i++)
            {
                if(programArgs[i].StartsWith("-"))
                { 
                    Debug.WriteLine("Found first option, exited: " + programArgs[i]);
                    break;
                }

                args.Add(programArgs[i]);
                string match = string.Join(":", args);

                if( Commands.ContainsKey(match))
                {   
                    command = Commands[match];
                    Debug.WriteLine(command.GetType().FullName);
                }
            } 
            
            Debug.Unindent();
            
            return command;
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
        private static void LoadCommands()
        {
            Debug.Indent();
            Debug.WriteLine("Loading commands ...");

            // Loads any command from Core
            LoadAssemblyCommands(Assembly.GetAssembly(typeof(CommandFactory)));

            DirectoryInfo[] templates = GetTemplatesDirectory().GetDirectories();

            foreach(DirectoryInfo template in templates)
            {
                Debug.WriteLine("Template: " + template.Name);
                Debug.WriteLine("Path: " + template.FullName);
            
                foreach(FileInfo file in template.GetFiles("*.dll"))
                {
                    LoadAssemblyCommands(Assembly.LoadFile(file.FullName));
                }
            }
        }

        /// <summary>
        /// Loads the command for a given 
        /// </summary>
        /// <param name="assembly"></param>
        private static void LoadAssemblyCommands(Assembly assembly)
        {
            Debug.Indent();
            Debug.WriteLine(assembly.FullName);

            foreach (Type type in assembly.GetTypes())
            {
                if( type.IsSubclassOf(typeof(Command)))
                {   
                    Command command = Activator.CreateInstance(type) as Command;
                    Debug.Indent();
                    Debug.WriteLine(command.GetType().FullName);
                    Debug.Indent();
                    var attributes = type.GetCustomAttributes();
                    
                    foreach(Attribute attribute in attributes)
                    {   
                        AddInfo(command, attribute as CommandInfo);
                        AddOption(command, attribute as CommandOption);
                    }
                    
                    Commands.Add(command.Info.Key, command);

                    Debug.Unindent();
                    Debug.Unindent();
                }
            }

            Debug.Unindent();
        }

        private static void AddInfo(Command command, CommandInfo info)
        {
            if( info != null )
            {
                command.Info = info;
                info.Debug();
            } 
        }

        private static void AddOption(Command command, CommandOption option)
        {
            if( option != null)
            {
                command.Options.Add(option);
                option.Debug();
            }
        }
    }
}

