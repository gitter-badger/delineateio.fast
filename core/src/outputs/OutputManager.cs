using System.Collections.Generic;

namespace Delineate.Fast.Core.Outputs
{
    public delegate void OutputEventHandler(object sender, OutputEventArgs e);

    /// <summary>
    /// Utility class for managing output
    /// </summary>
    public class OutputManager
    {
        public int CurrentIndent { get; private set; }

        public bool IsQuiet { get; set; }

        public event OutputEventHandler OnOutput;

        public void Indent() 
        {
            CurrentIndent++;
        }

        public void Indent(int number) 
        {
            CurrentIndent = CurrentIndent + number;
        }

        public void Unindent()
        {
            if(CurrentIndent > 0)
                CurrentIndent--;

            if(CurrentIndent < 0)
                CurrentIndent = 0;
        }

        public void Unindent(int number)
        {
            CurrentIndent = CurrentIndent - number;

            if(CurrentIndent < 0)
                CurrentIndent = 0;
        }

        public void SendNormal(string line = "", bool isNested = true)
        {
            Send(line, OutputLevel.Normal, isNested);
        }

        public void SendImportant(string line = "", bool isNested = true)
        {
            Send(line, OutputLevel.Important, isNested);
        }

        public void SendError(string line = "", int blanks = 0, bool isNested = true)
        {
            Send(line, OutputLevel.Error, isNested);
        }

        public void SendLink(string line = "", bool isNested = true)
        {
            Send(line, OutputLevel.Link, isNested);
        }
    
        public void SendWarning(string line = "", bool isNested = true)
        {
            Send(line, OutputLevel.Warning, isNested);
        }

        public void SendInfo(string line = "", bool isNested = true)
        {
            Send(line, OutputLevel.Info, isNested);
        }

        public void SendSuccess(string line = "", bool isNested = true)
        {
            Send(line, OutputLevel.Success, isNested);
        }

        public void SendBlank()
        {
            Send(string.Empty, OutputLevel.Normal, false);
        }

        private void Send(string line, OutputLevel level, bool isNested)
        {
            if(IsQuiet) return;
            
            List<string> lines = new List<string>();
            lines.Add(line);
            
            OutputEventHandler handler = OnOutput;   

            if (handler != null) 
            {
                OutputEventArgs e = new OutputEventArgs()
                    {
                        Lines = lines,
                        Level = level,
                        Indent = CurrentIndent,
                        IsNested = isNested
                    };

                handler(this, e);
            }
        }
    }
}
