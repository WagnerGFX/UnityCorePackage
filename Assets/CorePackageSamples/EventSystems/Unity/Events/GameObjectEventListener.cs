using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with two arguments: GameObject, object
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "GameObject Listener", 2)]
    public class GameObjectEventListener : BaseEventListener<GameObjectEventChannelSO, GameObject, object>
    { }
}
