using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows two arguments: string, object
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "String Event", order = 1)]
    public class StringEventChannelSO : BaseEventChannelSO<string, object> { } 
}
