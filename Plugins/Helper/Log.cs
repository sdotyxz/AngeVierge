using System.Collections.Generic;

public class Log
{
    public static void Debug(object message) { if (UnityEngine.Debug.isDebugBuild) UnityEngine.Debug.Log(message); }
    public static void Debug(object message, UnityEngine.Object context) { if (UnityEngine.Debug.isDebugBuild) UnityEngine.Debug.Log(message, context); }
    public static void Warning(object message) { if (UnityEngine.Debug.isDebugBuild) UnityEngine.Debug.LogWarning(message); }
    public static void Warning(object message, UnityEngine.Object context) { if (UnityEngine.Debug.isDebugBuild) UnityEngine.Debug.LogWarning(message, context); }
    
    public static void Error(object message)
    {
        _NotifyError(message.ToString());
        if (UnityEngine.Debug.isDebugBuild) UnityEngine.Debug.LogError(message);
    }
    public static void Error(object message, UnityEngine.Object context)
    {
        _NotifyError(context.ToString() + " - " + message);
        if (UnityEngine.Debug.isDebugBuild) UnityEngine.Debug.LogError(message, context);
    }
  
    public static void Exception(System.Exception exception)
    {
        _NotifyError(exception.ToString());
        if (UnityEngine.Debug.isDebugBuild) UnityEngine.Debug.LogException(exception);
    }
    public static void Exception(System.Exception exception, UnityEngine.Object context)
    {
        _NotifyError(context.ToString() + " - " + exception.ToString());
        if (UnityEngine.Debug.isDebugBuild) UnityEngine.Debug.LogException(exception, context);
    }

    private static System.Action<string> _errorHandler;
    public static void InitErrorHandler(System.Action<string> errorHandler)
    {
        _errorHandler = errorHandler;
    }

    private static void _NotifyError(string content)
    {
        if (_errorHandler != null) _errorHandler(content);
    }
}