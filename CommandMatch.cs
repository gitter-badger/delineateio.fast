using System;
using System.Reflection;

namespace Delineate.Fast
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class CommandMatch : Attribute
    {
        readonly string positionalString;
        
        // This is a positional argument
        public CommandMatch(String[] args)
        {
            Args = args;
        }
        
        public String[] Args{ get; private set;}
    }
}