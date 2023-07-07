using CorePackage.EventSystems.Unity.Debugging;
using CorePackageSamples.UnityEvents;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Attaches to the event channel like a parasite to debug information.
/// </summary>
public class DebugParasite : MonoBehaviour
{
    [SerializeField]
    BasicGameObjectEventChannelSO OnToggle;

    void OnEnable()
    {
        OnToggle.OnEventRaised += EventDebugUtilities.DebugRaisedEvent;
        OnToggle.OnEventRaisedWithNoListeners += EventDebugUtilities.DebugRaisedEventWithNoListeners;
        OnToggle.OnEventSubscribed += EventDebugUtilities.DebugSubscribed;
        OnToggle.OnEventUnsubscribed += EventDebugUtilities.DebugUnsubscribed;
    }

    void OnDisable()
    {
        OnToggle.OnEventRaised -= EventDebugUtilities.DebugRaisedEvent;
        OnToggle.OnEventRaisedWithNoListeners -= EventDebugUtilities.DebugRaisedEventWithNoListeners;
        OnToggle.OnEventSubscribed -= EventDebugUtilities.DebugSubscribed;
        OnToggle.OnEventUnsubscribed -= EventDebugUtilities.DebugUnsubscribed;
    }
}
