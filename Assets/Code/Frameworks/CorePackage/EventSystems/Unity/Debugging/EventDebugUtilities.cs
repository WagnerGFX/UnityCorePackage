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
            => InnerDebugRaisedEvent(eventChannel, null, null, null);

        public static void DebugRaisedEventWithNoListeners<TValue>(ScriptableObject eventChannel, TValue value)
            => InnerDebugRaisedEvent(eventChannel, value, null, null);

        public static void DebugRaisedEventWithNoListeners<TValue,TSender>(ScriptableObject eventChannel, TValue value, TSender sender)
            => InnerDebugRaisedEvent(eventChannel, value, sender, null);

        public static void DebugRaisedEvent(ScriptableObject eventChannel, Delegate[] invocationList)
            => InnerDebugRaisedEvent(eventChannel, null, null, invocationList);

        public static void DebugRaisedEvent<TValue>(ScriptableObject eventChannel, TValue value, Delegate[] invocationList)
            => InnerDebugRaisedEvent(eventChannel, value, null, invocationList);

        public static void DebugRaisedEvent<TValue, TSender>(ScriptableObject eventChannel, TValue value, TSender sender, Delegate[] invocationList)
            => InnerDebugRaisedEvent(eventChannel, value, sender, invocationList);

        public static void DebugSubscribed(ScriptableObject eventChannel, UnityAction action, bool success)
            => InnerDebugSubscription(eventChannel, true, action, success);

        public static void DebugSubscribed<TValue>(ScriptableObject eventChannel, UnityAction<TValue> action, bool success)
            => InnerDebugSubscription(eventChannel, true, action, success);

        public static void DebugSubscribed<TValue, TSender>(ScriptableObject eventChannel, UnityAction<TValue, TSender> action, bool success)
            => InnerDebugSubscription(eventChannel, true, action, success);

        public static void DebugUnsubscribed(ScriptableObject eventChannel, UnityAction action, bool success)
            => InnerDebugSubscription(eventChannel, false, action, success);

        public static void DebugUnsubscribed<TValue>(ScriptableObject eventChannel, UnityAction<TValue> action, bool success)
            => InnerDebugSubscription(eventChannel, false, action, success);

        public static void DebugUnsubscribed<TValue, TSender>(ScriptableObject eventChannel, UnityAction<TValue, TSender> action, bool success)
            => InnerDebugSubscription(eventChannel, false, action, success);


        private static void InnerDebugRaisedEvent(ScriptableObject eventChannel, object value, object sender, Delegate[] invocationList)
        {
            var debugInfo = new StringBuilder();

            //Event Channel
            debugInfo.Append($"Event Channel: {GetUnityObjectInfo(eventChannel)}");

            //Status
            if (invocationList is not null)
                debugInfo.Append($"\nRaised event with {invocationList.Length} listener(s)");
            else
                debugInfo.Append("\nRaised event with no listeners");

            //Sender
            if (sender is not null and UnityEngine.Object)
                debugInfo.Append($"\nSender: {GetUnityObjectInfo(sender)}");
            else if (sender is not null)
                debugInfo.Append($"\nSender: {sender}");

            //Value
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

            Debug.Log(debugInfo.ToString());
        }

        private static void InnerDebugSubscription(ScriptableObject eventChannel, bool isSubscribing, Delegate action, bool success)
        {
            var debugInfo = new StringBuilder();

            //Event Channel
            debugInfo.Append($"Event Channel: {GetUnityObjectInfo(eventChannel)}");

            //Subscription State
            debugInfo.Append(isSubscribing ? "\nSubscribed" : "\nUnsubscribed");
            debugInfo.Append(" with the status: ");
            debugInfo.Append(success ? "Success" : "Failed");

            //Action
            string targetOwnerInfo = GetUnityObjectInfo(action.Target);
            debugInfo.Append($"\n    => {targetOwnerInfo}{action.Target.GetType().FullName} :: {action.Method}");

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
