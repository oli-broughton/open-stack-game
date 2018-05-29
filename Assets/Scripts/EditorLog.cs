using System.Diagnostics;
using UnityEngine;

namespace OB
{
    /// <summary>
    /// Provides logging functionality that is only included when running in the editor.
    /// </summary>
    static public class EditorLog
    {

        /// <summary>
        /// Logs message to the Unity Console.
        /// </summary>
        /// <param name="message">Message.</param>
        [Conditional("UNITY_EDITOR")]
        public static void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }

        /// <summary>
        /// Logs message to the Unity Console.
        /// </summary>
        /// <param name="message">Message to log to the console.</param>
        /// <param name="context">Object context.</param>
        [Conditional("UNITY_EDITOR")]
        public static void Log(object message, Object context)
        {
            UnityEngine.Debug.Log(message, context);
        }

        /// <summary>
        /// Logs an error message to the Unity Console.
        /// </summary>
        /// <param name="message">Message to log to the console.</param>
        /// <param name="context">Object context.</param>
        [Conditional("UNITY_EDITOR")]
        public static void LogError(object message, Object context)
        {
            UnityEngine.Debug.LogError(message, context);
        }

        /// <summary>
        /// A variant of Debug.Log that logs an error message to the Unity Console.
        /// </summary>
        /// <param name="message">Message to log to the console.</param>
        [Conditional("UNITY_EDITOR")]
        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError(message);
        }


        /// <summary>
        ///  Logs an error message to the console when a specified property is null.
        /// </summary>
        /// <param name="propertyRef">Property reference to test for null.</param>
        /// <param name="propertyName">Property name to display to the console.</param>
        /// <param name="context">Context of the error message.</param>
        /// <typeparam name="PropertyType">Type of the property to test for null.</typeparam>
        [Conditional("UNITY_EDITOR")]
        public static void LogErrorOnNull<PropertyType>(PropertyType propertyRef, string propertyName, Object context) where PropertyType : UnityEngine.Object 
        {   
            if (propertyRef == null)
            {
                UnityEngine.Debug.LogError("Null Property Error : " + propertyName + " is null", context);
            }
        }

        /// <summary>
        /// Logs a warning to the Unity Console.
        /// </summary>
        /// <param name="message">Message to log to the console.</param>
        /// <param name="context">Object context.</param>
        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(object message, Object context)
        {
            UnityEngine.Debug.LogWarning(message, context);
        }

        /// <summary>
        /// Logs a warning to the Unity Console.
        /// </summary>
        /// <param name="message">Message to log to the console.</param>
        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(object message)
        {
            UnityEngine.Debug.LogWarning(message);
        }   
    }
}
