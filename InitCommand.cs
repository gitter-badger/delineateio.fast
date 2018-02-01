using System;
using System.IO;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    [CommandMatch(Key="init")]
    public sealed class InitCommand : Command
    {
        protected override void Prepare()
        {
            Node fastDir = Root.Add(".fast", NodeTypes.Directory, NodeOperations.Create);
            fastDir.Add("fast.yaml", NodeTypes.File, NodeOperations.Create);
            Node devDir = Root.Add("dev", NodeTypes.Directory, NodeOperations.Create);
            devDir.Add("tests", NodeTypes.Directory, NodeOperations.Create);
            Node opsDir = Root.Add("ops", NodeTypes.Directory, NodeOperations.Create);
            opsDir.Add("deploy", NodeTypes.Directory, NodeOperations.Create);
            opsDir.Add("provision", NodeTypes.Directory, NodeOperations.Create);
            opsDir.Add("tests", NodeTypes.Directory, NodeOperations.Create);
            Root.Add("tests", NodeTypes.Directory, NodeOperations.Create);
        }
    }
}