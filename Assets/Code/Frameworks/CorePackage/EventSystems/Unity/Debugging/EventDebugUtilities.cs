using System;
using System.Text;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.UIElements;

/// <summary>
/// Used to debug events by subscribing to BaseEventChannelSO debugging events.
/// </summary>
namespace CorePackage.EventSystems.Unity.Debugging
{
    public static class EventDebugUtilities
    {
        public static void DebugRaisedEventWithNoListeners(ScriptableObject eventChannel)
            => DebugRaisedEvent(eventChannel, null, null, null);
        public static void DebugRaisedEventWithNoListeners(ScriptableObject eventChannel, object value)
            => DebugRaisedEvent(eventChannel, value, null, null);
        public static void DebugRaisedEventWithNoListeners(ScriptableObject eventChannel, object value, object sender)
            => DebugRaisedEvent(eventChannel, value, sender, null);

        public static void DebugRaisedEvent(ScriptableObject eventChannel, Delegate[] invocationList)
            => DebugRaisedEvent(eventChannel, null, null, invocationList);
        public static void DebugRaisedEvent(ScriptableObject eventChannel, object value, Delegate[] invocationList)
            => DebugRaisedEvent(eventChannel, value, null, invocationList);

        public static void DebugRaisedEvent(ScriptableObject eventChannel, object value, object sender, Delegate[] invocationList)
        {
            var debugInfo = new StringBuilder();

            //Event Channel
            debugInfo.Append($"Raised Event Channel: {GetUnityObjectInfo(eventChannel)}");

            //Sender and Value
            if (sender is not null)
                debugInfo.Append($"\nSender: ");

            if (value is not null and UnityEngine.Object)
                debugInfo.Append($"\nValue: {GetUnityObjectInfo(value)}");

            else if (value is not null)
                debugInfo.Append($"\nValue: {value}");

            //Invocation List
            if (invocationList is not null)
            {
                debugInfo.Append($"\nInvocation List:");

                foreach (Delegate del in invocationList)
                {
                    string targetOwnerInfo = GetUnityObjectInfo(del.Target);
                    debugInfo.Append($"\n    => {targetOwnerInfo}{del.Target.GetType().FullName} :: {del.Method}");
                }
            }
            else
            {
                debugInfo.Append($"\nNo Listeners...");
            }
            

            Debug.Log(debugInfo.ToString());
        }

        private static string GetUnityObjectInfo(object target) => target switch
        {   // Extension methods don't follow Inheritance. Actual class must be defined.
            MonoBehaviour typedTarget => typedTarget.GetDebugInfo(),
            GameObject typedTarget => typedTarget.GetDebugInfo(),
            ScriptableObject typedTarget => typedTarget.GetDebugInfo(),
            UnityEngine.Object typedTarget => typedTarget.GetDebugInfo(),
            _ => target.GetDebugInfo(),
        };

    }
}
