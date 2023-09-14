using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with one argument: string
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Basic String Listener", 18)]
    public class BasicStringEventListener : BaseEventListener<BasicStringEventChannelSO, string>
    { }
}
