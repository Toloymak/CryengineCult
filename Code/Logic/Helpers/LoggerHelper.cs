
using System;
using System.Diagnostics.Tracing;

namespace CryEngine.Game.Helpers
{
    public class LoggerHelper
    {
        public void Log(string message, EventLevel level = EventLevel.Informational)
        {
            if (level == EventLevel.Informational)
            {
                CryEngine.Log.Info(message);
            }
            else if (level == EventLevel.Error)
            {
                CryEngine.Log.Error(message);
            }
        }
    }
}