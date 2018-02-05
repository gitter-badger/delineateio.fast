using System;
using System.IO;
using System.Collections.Generic;

namespace Delineate.Fast.Nodes
{
    /// <summary>
    /// Represents the root node in the command that will
    /// be used during the plan and Apply command methods
    /// </summary>
    public sealed class DirectoryNode : Node 
    { 
        public override void Plan(List<string> warnings)
        {
            if(Directory.Exists(WorkingDirectory.FullName))
            {   
                if(Operation == NodeOperation.Delete)
                    warnings.Add(Name);
            }
        }

        public override void Apply()
        {
            switch(this.Operation)
            {
                case NodeOperation.Create:
                    if(! Directory.Exists(WorkingDirectory.FullName))
                        Directory.CreateDirectory(WorkingDirectory.FullName);
                    break;

                case NodeOperation.Delete:
                    if(Directory.Exists(WorkingDirectory.FullName))
                        Directory.Delete(WorkingDirectory.FullName, true);
                    break;
                    
                default:    
                    throw new ArgumentException();
            }
        }
    }
}