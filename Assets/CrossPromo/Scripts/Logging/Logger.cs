using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace CrossPromo.Logging
{

    public static class Logger
    {
        private enum LogType { Message, Warning, Error }

        private const string Prefix = "[CrossPromo] ";

        [Conditional("CROSSPROMO_LOG_ENABLED")]
        public static void Log(string msg)
        {
            LogInternal(LogType.Message, msg);
        }

        [Conditional("CROSSPROMO_LOG_ENABLED")]
        public static void LogWarning(string msg)
        {
            LogInternal(LogType.Warning, msg);
        }

        [Conditional("CROSSPROMO_LOG_ENABLED")]
        public static void LogError(string msg)
        {
            LogInternal(LogType.Error, msg);
        }

        [Conditional("CROSSPROMO_LOG_ENABLED")]
        private static void LogInternal(LogType type, string msg)
        {
            string fullMsg = Prefix + msg;
            switch (type)
            {
                case LogType.Error:
                    Debug.LogError(fullMsg);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(fullMsg);
                    break;
                case LogType.Message:
                    Debug.Log(fullMsg);
                    break;
            }
        }
    }
}
