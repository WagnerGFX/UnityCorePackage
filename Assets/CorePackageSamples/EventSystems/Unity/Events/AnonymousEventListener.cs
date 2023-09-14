using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with no arguments.
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Anonymous Listener", 21)]
    public class AnonymousEventListener : BaseEventListener<AnonymousEventChannelSO>
    { }
}
