using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Help
{
    public abstract class BaseBuilder
    {
        protected Command Command { get; set; }

        public BaseBuilder(Command command)
        {
            Command = command;
        }

        public abstract void Output();

        protected void AddHeader(string line)
        {
            Command.Outputs.Indent();
            Command.Outputs.SendBlank();
            Command.Outputs.SendNormal(line);
            Command.Outputs.Unindent();
            Command.Outputs.SendBlank();
        }
    }
}