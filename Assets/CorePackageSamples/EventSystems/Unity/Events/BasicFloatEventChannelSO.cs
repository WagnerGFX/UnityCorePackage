using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows one argument: float
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Basic Float Event", order = 18)]
    public class BasicFloatEventChannelSO : BaseEventChannelSO<float>
    { }
}
