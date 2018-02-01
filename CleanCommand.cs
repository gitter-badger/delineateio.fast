using System;
using System.IO;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    [CommandMatch(Key="clean")]
    public sealed class CleanCommand : Command
    {
        protected override void Prepare()
        {
            Root.Add("fast.config", NodeTypes.File, NodeOperations.Delete);
            Root.Add("dev", NodeTypes.Directory, NodeOperations.Delete);
            Root.Add("ops", NodeTypes.Directory, NodeOperations.Delete);
            Root.Add("tests", NodeTypes.Directory, NodeOperations.Delete);
        }
    }
}