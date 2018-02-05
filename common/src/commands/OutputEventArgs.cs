using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Fast.Commands
{
    /// <summary>
    /// The Command message events args 
    /// </summary>
    public sealed class OutputEventArgs: EventArgs
    {
        /// <summary>
        /// Lines of the text 
        /// </summary>
        /// <returns>Returns the list of lines</returns>
        public IList<string> Lines { get; set; }

        /// <summary>
        /// The color to be used when writing out the text
        /// </summary>
        /// <returns></returns>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// The indent level to use
        /// </summary>
        /// <returns>Returns the indent value</returns>
        public int Indent { get; set; }

        /// <summary>
        /// This is the number of blanks to write
        /// </summary>
        /// <returns>The number of blank lines to write</returns>
        public int Blanks { get; set; }
    } 
}
