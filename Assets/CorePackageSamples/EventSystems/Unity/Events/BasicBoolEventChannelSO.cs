using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows one argument: boolean
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Basic Bool Event", order = 18)]
    public class BasicBoolEventChannelSO : BaseEventChannelSO<bool> { } 
}
