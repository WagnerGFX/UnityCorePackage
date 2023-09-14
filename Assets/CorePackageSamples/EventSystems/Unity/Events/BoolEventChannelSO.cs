using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows two arguments: boolean, object
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Bool Event", order = 1)]
    public class BoolEventChannelSO : BaseEventChannelSO<bool, object>
    { }
}
