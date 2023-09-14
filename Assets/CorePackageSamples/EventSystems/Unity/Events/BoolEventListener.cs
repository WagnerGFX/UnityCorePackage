using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with two arguments: boolean, object
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Bool Listener", 1)]
    public class BoolEventListener : BaseEventListener<BoolEventChannelSO, bool, object>
    { }
}
