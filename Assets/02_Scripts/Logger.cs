using UnityEngine;

public class Logger
{
    public static void WriteLog(string logString)
    {
#if UNITY_EDITOR
        //if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        Debug.Log(logString);
#endif
    }

    public static void WriteLogErr(string logString)
    {
#if UNITY_EDITOR
        //if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        Debug.LogError(logString);
#endif
    }

    public static void WriteLogWarning(string logString)
    {
#if UNITY_EDITOR
        //if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        Debug.LogWarning(logString);
#endif
    }
}
