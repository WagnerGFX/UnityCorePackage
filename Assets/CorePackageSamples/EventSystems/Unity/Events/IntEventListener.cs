using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with two arguments: int, object
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Int Listener", 1)]
    public class IntEventListener : BaseEventListener<IntEventChannelSO, int, object>
    {

    }
}
