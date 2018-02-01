using System;
using System.Reflection;

namespace Delineate.Cloud.Fast
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class CommandMatch : Attribute
    {   
        public string Key{ get; set;}
    }
}