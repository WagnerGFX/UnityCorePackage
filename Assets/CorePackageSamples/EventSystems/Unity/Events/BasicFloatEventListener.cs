using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with one argument: float
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Basic Float Listener", 18)]
    public class BasicFloatEventListener : BaseEventListener<BasicFloatEventChannelSO, float>
    { }
}
