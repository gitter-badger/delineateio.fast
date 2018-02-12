namespace Delineate.Fast.Core.Logging
{
    /// <summary>
    /// Interface for the logging 
    /// </summary>
    public interface ILogs
    {
        /// <summary>
        /// Operation to log to the registered listeners
        /// </summary>
        /// <param name="text">The text to record</param>
        void Log(string text, params string[] values);
    }
}