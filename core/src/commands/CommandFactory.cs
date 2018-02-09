using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

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
            Debug.Indent();
            Debug.WriteLine("Command requested: " + string.Join(", ", programArgs));

            if(Commands == null || Commands.Count == 0)
                LoadCommands();

            Command command = CreateCommand(programArgs);
            Debug.Unindent();

            return command; 
        }

        /// <summary>
        /// Method to find the command and instantiate an instance
        /// </summary>
        /// <param name="programArgs">The program args to be used to find the command</param>
        /// <returns>Returns an instance of teh command</returns>
        private static Command CreateCommand(string[] programArgs)
        {
            Type commandType = typeof(DefaultCommand);
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
                    commandType = Commands[match];
                    Debug.WriteLine(commandType.GetType().FullName);
                }
            } 
            
            Debug.Unindent();
            
            Command command = Activator.CreateInstance(commandType) as Command;
            Debug.WriteLine("Created command: " + commandType.GetType().FullName);

            SetFromAttributes(command);

            return command;
        }
        
        #region Attribute Loading
 
        private static void SetFromAttributes(Command command)
        {
            SetCommandInfo(command);
            SetCommandOptions(command);
        }

        private static void SetCommandOptions(Command command)
        {
            var attributes = command.GetType().GetCustomAttributes(typeof(CommandOption));
            if (attributes != null)
            {
                Debug.Indent();

                foreach(CommandOption option in attributes)
                {   
                    command.Options.Add(option);
                    Debug.WriteLine("Added option: " + option.Key + option.Aliases);
                }

                Debug.Unindent();
            }
        }
        
        private static void SetCommandInfo(Command command)
        {
            var attributes = command.GetType().GetCustomAttributes(typeof(CommandInfo));
            if (attributes != null)
            {
                foreach(CommandInfo info in attributes)
                {   
                    command.Info = info;
                    Debug.WriteLine("CommandInfo added");
                }
            }
        }

        #endregion

        private static DirectoryInfo GetTemplatesDirectory()
        {
            string path = string.Format("{0}{1}{2}",
                                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                Path.DirectorySeparatorChar,
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
            Debug.Indent();

            foreach (Type type in assembly.GetTypes())
            {
                var attributes = type.GetCustomAttributes(typeof(CommandInfo));
                if (attributes != null)
                {
                    foreach(CommandInfo info in attributes)
                    {   
                        if(! info.IsDefault)
                        {
                            Commands.Add(info.Key, type);
                            Debug.WriteLine("Key: " + info.Key +", Type: " + type.FullName);
                        }
                    }
                }
            }

            Debug.Unindent();
            Debug.Unindent();
        }

        public static SortedDictionary<string, Type> ReloadTemplates()
        {
            Commands.Clear();
            LoadCommands();
            return Commands;
        }
    }
}

