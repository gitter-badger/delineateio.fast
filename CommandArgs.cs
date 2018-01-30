using System; 
using System.Collections;

namespace Delineate.Fast
{
    public class CommandArgs: IComparable<CommandArgs>
    {
        public CommandArgs(string[] args)
        {
            String[] cleanArgs = new String[args.Length];
            
            for(int i = 0; args.Length > i; i++)
            {
                cleanArgs[i] = args[i].ToLower();
            }

            Values = cleanArgs;
        }
        
        public int CompareTo(CommandArgs args)
        {
            for(int i =0; args.Values.Length > i ; i++)
            {
                if(Values[i] != args.Values[i]) return 1; 
            }

            return 0;
        }
        
        public string[] Values {get; private set;}
    }
}