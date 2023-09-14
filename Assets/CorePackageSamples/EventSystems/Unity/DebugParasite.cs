using CorePackage.EventSystems.Unity.Debugging;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Attaches to the event channel like a parasite to debug information.
    /// </summary>
    public class DebugParasite : MonoBehaviour
    {
        [SerializeField]
        BasicGameObjectEventChannelSO _onToggle;

        void OnEnable()
        {
            _onToggle.OnEventRaised += EventDebugUtilities.DebugRaisedEvent;
            _onToggle.OnEventRaisedWithNoListeners += EventDebugUtilities.DebugRaisedEventWithNoListeners;
            _onToggle.OnEventSubscribed += EventDebugUtilities.DebugSubscribed;
            _onToggle.OnEventUnsubscribed += EventDebugUtilities.DebugUnsubscribed;
        }

        void OnDisable()
        {
            _onToggle.OnEventRaised -= EventDebugUtilities.DebugRaisedEvent;
            _onToggle.OnEventRaisedWithNoListeners -= EventDebugUtilities.DebugRaisedEventWithNoListeners;
            _onToggle.OnEventSubscribed -= EventDebugUtilities.DebugSubscribed;
            _onToggle.OnEventUnsubscribed -= EventDebugUtilities.DebugUnsubscribed;
        }
    }
}
