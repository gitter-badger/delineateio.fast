using System;
using System.IO;
using System.Collections.Generic;
using System.Resources;

namespace Delineate.Cloud.Fast
{
    public abstract class Command
    {
        protected CommandOptions Options { get; set; }

        protected CommandArgs Args { get; set; } 

        public CommandArgsParser Parser { get; set; }

        protected Node Root { get; set; }

        protected bool CanApply { get; set; }
    
        public Command()
        {
            Options = new CommandOptions();
            Args = new CommandArgs();
            Parser = new CommandArgsParser();
            Root = Node.CreateRoot();
        }

        public void Execute(ProgramArgs programArgs)
        { 
            AddOptions(); // Add command options
            Parser.Parse(programArgs, Options, Args);

            if( Parser.HasErrors)
            {
                Help();
            }
            else
            {
                Prepare();
                Plan();
                if(CanApply)
                { 
                    Apply();
                }
            }
        }

        private void Help()
        {
            
        }

        protected void AddOptions(){}

        protected virtual void Prepare() {}

        protected virtual void Plan() 
        {
            List<string> warnings = new List<string>();

            foreach(Node node in Root.Nodes)
            {
                if(node.Type == NodeTypes.File && File.Exists(node.Name))
                    warnings.Add(node.Name);
            }

            if(warnings.Count > 0)
            {
                ConsoleWriter.WriteLine("Planning ...");
                ConsoleWriter.WriteLines(warnings, ConsoleColor.Yellow, 1, true);

                if(Args.IsForced)
                {
                    ConsoleWriter.WriteBlankLine();
                    ConsoleWriter.WriteLine("Warnings have been overriden");
                    CanApply = true;
                }    
                else
                {
                    ConsoleWriter.WriteBlankLine();
                    ConsoleWriter.WriteLine("Commmand could not be completed.  Please review the warnings.", ConsoleColor.Red);
                }
            }
            else
            {
                CanApply = true;
            }
        }

        protected virtual void Apply()
        {
            ConsoleWriter.WriteBlankLine();
            ConsoleWriter.WriteLine("Applying ...");
            
            Apply(Root.Nodes, new DirectoryInfo(Environment.CurrentDirectory));
        }

        protected void Apply(List<Node> nodes, DirectoryInfo directory, int indent = 1)
        {
            if(nodes != null && nodes.Count > 0)
            {
                foreach(Node node in nodes)
                {
                    switch (node.Type)
                    {
                        case NodeTypes.Root:
                            break;
                        case NodeTypes.File: 
                            string path = string.Format("{0}{1}{2}", directory.FullName, Path.DirectorySeparatorChar, node.Name);
                            File.Create(path);
                            ConsoleWriter.WriteLine(node.Name, ConsoleColor.Green, indent, true);
                            break;
                        case NodeTypes.Directory:
                            DirectoryInfo info = directory.CreateSubdirectory(node.Name);
                            ConsoleWriter.WriteLine(node.Name, ConsoleColor.Green, indent, true);
                            int level = indent + 1;
                            Apply(node.Nodes, info, level);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
