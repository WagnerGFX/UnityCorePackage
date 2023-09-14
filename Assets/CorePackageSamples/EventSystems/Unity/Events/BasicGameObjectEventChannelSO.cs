using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows one argument: GameObject
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Basic GameObject Event", order = 19)]
    public class BasicGameObjectEventChannelSO : BaseEventChannelSO<GameObject>
    { }
}
