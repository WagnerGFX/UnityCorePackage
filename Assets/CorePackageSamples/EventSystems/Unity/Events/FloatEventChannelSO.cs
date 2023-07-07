using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows two arguments: float, object
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Float Event", order = 1)]
    public class FloatEventChannelSO : BaseEventChannelSO<float, object> { } 
}
