using System.IO;

namespace Delineate.Fast
{
    public abstract class Command
    {
        public void Execute(CommandArgs args)
        {
            Prepare(args);
            Plan(args);
            Apply(args);
        }
    
        protected virtual void Prepare(CommandArgs args) {}

        protected virtual void Plan(CommandArgs args) {}

        protected virtual void Apply(CommandArgs args) {}
    }
}
