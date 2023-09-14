using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with two arguments: object, object
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Object Listener", 2)]
    public class ObjectEventListener : BaseEventListener<ObjectEventChannelSO, object, object>
    { }
}
