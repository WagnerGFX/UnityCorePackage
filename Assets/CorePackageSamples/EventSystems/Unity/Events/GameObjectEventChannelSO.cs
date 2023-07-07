using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows two arguments: GameObject, object
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "GameObject Event", order = 2)]
    public class GameObjectEventChannelSO : BaseEventChannelSO<GameObject, object> { } 
}
