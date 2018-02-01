using System;
using System.Reflection;

namespace Delineate.Cloud.Fast
{
    public sealed class CommandOption
    {
        public string Description { get; set; }
        
        public string[] Alias { get; set; }

        public bool HasValue { get; set; }

        public bool IsMandatory { get; set; }

        public CommandOptionTypes Type { get; set; }
    }
}