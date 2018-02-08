using System;

namespace Delineate.Fast.Core.Tools
{
    /// <summary>
    /// Encapulsates the results of the tool
    /// </summary>
    public class ToolVersion
    {
        /// <summary>
        /// Raw version returned returned by the rool
        /// </summary>
        /// <returns></returns>
        public string Raw { get; private set; }

        /// <summary>
        /// Stripped version of the version number
        /// </summary>
        /// <returns></returns>
        public string Version { get; protected set; }
        
        /// <summary>
        /// Required fo derived class to be used as T
        /// </summary>
        public ToolVersion(){}

        public ToolVersion(string raw)
        {
            Raw = raw;
            Transform();
        }

        /// <summary>
        /// Performs any tarnsformation required to 
        /// get the version into a clean state
        /// </summary>
        /// <remarks>
        /// Default implementation assumes no cleaning required
        /// </remarks>
        protected virtual void Transform()
        {
            Version = Raw;
        }
    }
}