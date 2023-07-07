using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows one argument: object. Useful to send the event caller as argument.
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Basic Object Event", order = 19)]
    public class BasicObjectEventChannelSO : BaseEventChannelSO<object> { } 
}
