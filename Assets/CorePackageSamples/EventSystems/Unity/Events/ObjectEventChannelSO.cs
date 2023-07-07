using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows two arguments: object, object
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Object Event", order = 2)]
    public class ObjectEventChannelSO : BaseEventChannelSO<object, object> { } 
}
