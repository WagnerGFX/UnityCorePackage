using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with one argument: int
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Basic Int Listener", 18)]
    public class BasicIntEventListener : BaseEventListener<BasicIntEventChannelSO, int>
    {

    }
}
